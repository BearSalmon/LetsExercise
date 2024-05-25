
pose = {
    "arm1":  # 很像在舉重
    {
        "path": "/PoseDataset/arms/arm1.txt",
        "check_angle": [
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

    "arm2":  # 手臂90度往前伸
    {
        "path": "/PoseDataset/arms/arm2.txt",
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
    "arm3":  # 鋤頭式
    {
        "path": "/PoseDataset/arms/arm3.txt",
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
    "arm4":  # 拍翅膀
    {
        "path": "/PoseDataset/arms/arm4.txt",
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
    "arm5":  # 畫出大圓
    {
        "path": "./video_lmlist/arms/arm5.txt",
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
                "ref_point": "right_shoulder"
            }
        ],
    },
    
    
}

def get_pose_db():
    return pose


def find_pose_key_by_path(target_path):
    for key, value in pose.items():
        if value["path"] == target_path:
            return key
    return None