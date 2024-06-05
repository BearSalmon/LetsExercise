using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public string nowLevel ;
    public TextMeshProUGUI poseSetName;
    public TextMeshProUGUI duration;
    DBUtils dBUtils;
    User user;
    PoseSet poseSet;
    Record record;
    DateTime currDate = DateTime.Now;

    ButtonEvent buttonEvent;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        updateLevel("easy");
    }
    float exchageLevelval(string level)
    {
        if (level == "easy") return 1;
        else if (level == "medium") return 1.5f;
        else return 2;
    }

    public void updateCalendarRecord()
    {
        string serchTerm = "";
        if (currDate.Month < 10)
        {
            serchTerm = currDate.Year.ToString() + "0" + currDate.Month.ToString();
        }
        else
        {
            serchTerm = currDate.Year.ToString() + currDate.Month.ToString();
        }
        if (currDate.Day < 10)
        {
            serchTerm += "0" + currDate.Day.ToString();
        }
        else
        {
            serchTerm += currDate.Day.ToString();
        }
        record = dBUtils.GetRecordByNameAndDate(dBUtils.nowPlayer, serchTerm);
        record.Duration += GetDuration(nowLevel);
        record.Parts += poseSet.Part;
        dBUtils.UpdateRecord(record);
    }


    public void updateUser()
    {
        user.Duration += GetDuration(nowLevel);
        dBUtils.UpdateUser(user);
    }


    int GetDuration(string level)
    {
        int numOfPose = poseSet.NumberOfGesture;
        if (level == "easy")
        {
            return 60 * numOfPose;
        }
        else if (level == "medium") return 75 * numOfPose;
        else return 90 * numOfPose;
    }

    public void updateLevel(string level)
    {
        poseSet = dBUtils.GetPoseSetById(buttonEvent.poseSetID);
        nowLevel = level;
        int d = GetDuration(nowLevel);
        string min = (d / 60).ToString();
        string sec = (d % 60).ToString();

        duration.text = min + "m " + sec + "s";


        poseSetName.text = poseSet.PoseSetName;
    }
}
