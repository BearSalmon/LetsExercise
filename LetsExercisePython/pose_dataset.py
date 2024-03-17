
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
        ]
    },

    "手臂90度往前伸":
    {
        "path" : "./video_lmlist/arms/arm2.txt",
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
    },
    "鋤頭式":
    {
        "path" : "./video_lmlist/arms/arm3.txt",
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
    },
    "拍翅膀":
    {
        "path" : "./video_lmlist/arms/arm4.txt",
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
    },
    "畫出大圓":
    {
        "path" : "./video_lmlist/arms/arm5.txt",
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
    },
    "鋤頭式":
    {
        "path" : "./video_lmlist/abs/abs1.txt",
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
    }
    
}

def get_pose_db():
    return pose