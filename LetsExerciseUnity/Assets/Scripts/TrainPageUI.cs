using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TrainPageUI : MonoBehaviour
{
    public TextMeshProUGUI poseSetName;
    public TextMeshProUGUI calories;
    public TextMeshProUGUI duration;
    public TextMeshProUGUI numberOfGesture;
   

    int number;
    int nowSelect = 1;

    DBUtils dBUtils;

    PoseSet poseSet;

    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        number = 2; // 共有幾套動作
        updateTrainSet(nowSelect);
    }


    public void nextOption()
    {
        nowSelect++;
        if (nowSelect > number)
        {
            nowSelect = 1;
        }
        updateTrainSet(nowSelect);
    }

    public void backOption()
    {
        nowSelect--;
        if (nowSelect < 0)
        {
            nowSelect = number;
        }
        updateTrainSet(nowSelect);

    }

    public void updateTrainSet(int nowSelect)
    {
        poseSet = dBUtils.GetPoseSetById(nowSelect);
        poseSetName.text = poseSet.PoseSetName;
        calories.text = poseSet.Calories.ToString();
        duration.text = poseSet.Duration.ToString();
        numberOfGesture.text = poseSet.NumberOfGesture.ToString();
        
        
        
    }

}
