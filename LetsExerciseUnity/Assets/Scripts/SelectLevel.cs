using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public string nowLevel ;
    public TextMeshProUGUI poseSetName;
    public TextMeshProUGUI calories;
    public TextMeshProUGUI duration;
    DBUtils dBUtils;
    PoseSet poseSet;

    ButtonEvent buttonEvent;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        updateLevel("easy");
    }
    float exchageLevelval(string level)
    {
        if (level == "easy") return 1;
        else if (level == "medium") return 1.5f;
        else return 2;
    }

    public void updateLevel(string level)
    {
        poseSet = dBUtils.GetPoseSetById(buttonEvent.poseSetID);
        nowLevel = level;
        calories.text = (poseSet.Calories * exchageLevelval(level)).ToString() + " cal";
        duration.text = (poseSet.Duration * exchageLevelval(level)).ToString() + " s";

        poseSetName.text = poseSet.PoseSetName;
    }
}
