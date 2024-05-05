import cv2
import PoseModule
# from cvzone.PoseModule import PoseDetector
from cvzone.HandTrackingModule import HandDetector
import socket
from utils import find_angle, get_landmark_features , get_Wrong_Message , get_WrongPart_Message
from pose_dataset import get_pose_db
import json
import time
import os


if __name__ == "__main__":

    # 取得資料庫
    pose_db = get_pose_db()

    check_point = pose_db["很像在舉重"]["check_angle"]
    file_path = pose_db["很像在舉重"]["path"]
    exact_path = os.getcwd() + file_path

    with open(exact_path, "r") as lm_file:
        lines = lm_file.readlines()

    #tcp
    tcp_ip = '127.0.0.1'
    tcp_port = 5066
    address = (tcp_ip,tcp_port)

    #udp
    udp_ip = '127.0.0.1'
    udp_port_hand = 5052
    udp_port_angle = 5051
    udp_port_pos = 5054
    udp_port_wrongPart = 5056

    recv_ip = '127.0.0.1'  # Listen on all network interfaces
    recv_port = 1234  # Port number

    # Communication
    udp_sock_for_counter = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)  # UDP
    udp_sock_for_counter.setblocking(False)  # 將此socket設成非阻塞
    udp_sock_for_counter.bind((recv_ip, recv_port))
    serverAddressPort_hand = (udp_ip, udp_port_hand)
    serverAddressPort_angle = (udp_ip, udp_port_angle)
    serverAddressPort_pos = (udp_ip, udp_port_pos)
    serverAddressPort_wrongPart = (udp_ip, udp_port_wrongPart)

    # Parameters
    width, height = 360, 480

    # Webcam
    cap = cv2.VideoCapture(0)
    if not cap.isOpened():
        print("Cannot open camera")
        exit()

    # Get the actual width and height of the webcam frames
    # 640 * 480
    frame_width = int(cap.get(3))
    # print(frame_width)
    frame_height = int(cap.get(4))

    # Pose Detector
    pose_detector = PoseModule.PoseDetector()
    hand_detector = HandDetector(detectionCon=0.8, maxHands=2)

    dict_features = {
        'right_shoulder': 11,
        'right_elbow'   : 13,
        'right_wrist'   : 15,
        'right_hip'     : 23,
        'right_knee'    : 25,
        'right_ankle'   : 27,
        'right_foot'    : 31,
        'left_shoulder': 12,
        'left_elbow'   : 14,
        'left_wrist'   : 16,
        'left_hip'     : 24,
        'left_knee'    : 26,
        'left_ankle'   : 28,
        'left_foot'    : 32,
        'nose' : 0
    }

    wrong_message = "nice"
    counter = 0 #video_counter
    loop_cnt = 0

    while True:
        # if counter == 0:
        #     loop_cnt += 1
        #     print(loop_cnt)
        # if counter >= len(lines):
        #     counter = 0
        #     loop_cnt += 1
        success, img = cap.read()
        # 640 * 480
        # img = cv2.resize(img,(480, 720))
        img = cv2.flip(img, 1)

        # crop camera
        img = img[(frame_height - height) // 2:(frame_height + height) // 2, (frame_width - width) // 2:(frame_width + width) // 2]

        # 抓身體的點
        img = pose_detector.findPose(img, draw=True)
        lmList, bboxInfo = pose_detector.findPosition(img, draw=True)

        # cut the camera by bounding box
        # img = pose_detector.cropCamera(img, bboxInfo["upper_left_corner"][0] - 50, bboxInfo["upper_left_corner"][1] - 50,
        #                                bboxInfo["width"] + 100, bboxInfo["height"] + 200)

        #tcp
        img_data = {'image': cv2.imencode('.jpg', img)[1].ravel().tolist()}
        data = json.dumps(img_data)
        #準備連線
        tcp_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM) # TCP
        tcp_sock.connect(address)
        #傳送資料
        tcp_sock.sendall(bytes(data,encoding='utf-8'))

        # 傳送手部資料
        if lmList and bboxInfo :  
            index_finger = lmList[19][0], lmList[19][1] #right hand
            index_finger_json = json.dumps(index_finger)
            udp_sock_for_counter.sendto(str.encode(index_finger_json), serverAddressPort_hand)

        # Receive data from unity
        received_data = ""
        try:
            data_from_unity, addr = udp_sock_for_counter.recvfrom(1024)  # Buffer size is 1024 bytes
            received_data = data_from_unity.decode()
            #print(f"Received data from {addr}: {received_data}")
        except:
            pass

        # if received_data == "start":
        #     loop_cnt = 0
        #     seconds1 = time.time()
        # elif received_data == "stop":
        #     print(loop_cnt)
        #     seconds2 = time.time()
        #     print(seconds2 - seconds1)
        #     break

        if received_data :
            if received_data == "start":
                print(received_data)

            else:
                counter = int(received_data)

                points = lines[counter].split(',')
                video_lmlist = [[int(point) for point in points[i:i+3]] for i in range(0, len(points)-1, 3)]
                wrongPart_message = ""

                for index in range(len(check_point)):
                    point1,point2,ref_point = get_landmark_features(lmList,dict_features,check_point[index])
                    video_point1,video_point2,video_ref_point = get_landmark_features(video_lmlist,dict_features,check_point[index])
                    offset_angle = find_angle(point1,point2,ref_point)
                    video_offset_angle = find_angle(video_point1,video_point2,video_ref_point)
                    wrong = offset_angle - video_offset_angle
                    if wrong > 20 or abs(wrong) > 20:
                        wrongPart_message += get_WrongPart_Message(check_point[index],dict_features)
                        wrong_message = get_Wrong_Message(check_point[index],wrong,dict_features)
                    else :
                        wrong_message = "nice"
                    
                # udp
                udp_sock_for_counter.sendto(str.encode(str(wrong_message)), serverAddressPort_angle)
                udp_sock_for_counter.sendto(str.encode(str(wrongPart_message[:-1])), serverAddressPort_wrongPart)

        #print(lmList)
        kp_inside = 0
        for i in range(len(lmList)):
            if 0 <= lmList[i][0] <= width and 0 <= lmList[i][1] <= height:
                kp_inside += 1
        if kp_inside == len(lmList):
            udp_sock_for_counter.sendto(str.encode(""), serverAddressPort_pos)
        else:
            udp_sock_for_counter.sendto(str.encode("Align your body to the border"), serverAddressPort_pos)


        #cv2.imshow("Image", img)
        tcp_sock.close()

        # time.sleep(0.003)
        if cv2.waitKey(1) == ord('q'):
            break  # press q to quit

    # 釋放該攝影機裝置
    cap.release()
    cv2.destroyAllWindows()