import cv2
import numpy as np
import math

def find_angle(p1, p2, ref_pt):
    p1_ref = p1 - ref_pt
    p2_ref = p2 - ref_pt

    cos_theta = (np.dot(p1_ref,p2_ref)) / (1.0 * np.linalg.norm(p1_ref) * np.linalg.norm(p2_ref))
    theta = np.arccos(np.clip(cos_theta, -1.0, 1.0))
            
    degree = int(180 / np.pi) * theta

    return int(degree)

def get_landmark_array(pose_landmark, key):
    denorm_x = int(pose_landmark[key][0])
    denorm_y = int(pose_landmark[key][1])

    return np.array([denorm_x, denorm_y])




def get_landmark_features(kp_results, dict_features, feature):
    point1 = get_landmark_array(kp_results,dict_features[feature["point1"]])
    point2 = get_landmark_array(kp_results,dict_features[feature["point2"]])
    ref_point = get_landmark_array(kp_results,dict_features[feature["ref_point"]])

    return point1,point2,ref_point

dict_features_return = {
    11 : 'right shoulder',
    13 : 'right elbow',
    15 : 'right wrist' ,
    23 : 'right hip',
    25 : 'right knee',
    27 : 'right ankle',
    31 : 'right foot',
    12 : 'left shoulder',
    14 : 'left elbow',
    16 : 'left wrist', 
    24 : 'left hip',
    26 : 'left knee',
    28 : 'left ankle',
    32 : 'left foot',
    0 : 'nose'
}
    
def get_Wrong_Message(index,check_point,wrong_offset,video_offset_angle,dict_features):
    wrong_message = str(index) + ":"
    ref_point = dict_features[check_point["ref_point"]]

    if (check_point["ref_point"] == "right_elbow" or check_point["ref_point"] == "left_elbow"):
        if video_offset_angle > 170:
            wrong_message += "Your " + dict_features_return[ref_point] + "is not straight enough"
        else :
            wrong_message += "Your " + dict_features_return[ref_point] + " has "+str(wrong_offset) +" offset"
    elif check_point["ref_point"] == "left_shoulder" or check_point["ref_point"] == "right_shoulder":
        if wrong_offset < 0 :
            wrong_message += "Please raise your " + dict_features_return[ref_point] + " higher"
        else :
            wrong_message += "Please lower your " + dict_features_return[ref_point]


    return wrong_message
    