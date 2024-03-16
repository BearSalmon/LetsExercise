
pose = {
    "pose_name":
    {
        "path" : "./video_lmlist/arms/arm1.txt",
        "check_angle":[
            {
                "point1": "left_shoulder",
                "point2": "left_wrist",
                "ref_point":"left_elbow"
            },
            {
                "point1": "right_shoulder",
                "point2": "right_wrist",
                "ref_point":"right_elbow"
            },
            {
                "point1": "left_elbow",
                "point2": "left_hip",
                "ref_point":"left_shoulder"
            },
            {
                "point1": "right_elbow",
                "point2": "right_hip",
                "ref_point":"right_shoulder"
            }
        ],
        "video_size":{
            "width" :1920,
            "height":1080
        }
    }
    
}

def get_pose_db():
    return pose