using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using System;

public class PlanPageUI : MonoBehaviour
{
    // Start is called before the first frame update
    DBUtils dBUtils;
    ButtonEvent buttonEvent;

    public GameObject Pass;
    public GameObject UnPass;
    public TextMeshProUGUI duration;

    public RawImage arms;
    public RawImage abs;
    public RawImage legs;
    public RawImage buttocks1;
    public RawImage buttocks2;

    User user;
    Record record;
    DateTime currDate = DateTime.Now;
    int d;

    void Start()
    {
        
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        setPlanPassOrNot();
        setPlanSet();
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
        record.Duration += d;
        record.Parts += user.PreferPart + ",";
        dBUtils.UpdateRecord(record);
    }


    public void updateUser()
    {
        user.Duration += d;
        user.HasUnfinishedPlan = false;
        dBUtils.UpdateUser(user);
    }

    void setPlanPassOrNot()
    {
        if (user.HasUnfinishedPlan == true)
        {
            Pass.SetActive(false);
            UnPass.SetActive(true);
        }
        else
        {
            Pass.SetActive(true);
            UnPass.SetActive(false);
        }
    }


    int GetDuration(string level,int numOfPose )
    {
        if (level == "Easy")
        {
            return 60 * numOfPose;
        }
        else if (level == "Medium") return 75 * numOfPose;
        else return 90 * numOfPose;
    }

    void setPlanSet()
    {
        string[] poseNames;
        poseNames = user.RecommendationPoseSet.TrimEnd(',').Split(',');

        d = GetDuration(user.Level,poseNames.Length);
        string min = (d / 60).ToString();
        string sec = (d % 60).ToString();
        duration.text = min + "m " + sec + "s";

        Color color;
        ColorUtility.TryParseHtmlString("#".ToString() + "CCC4C4", out color);

        Color color2;
        ColorUtility.TryParseHtmlString("#".ToString() + "FFC9AA", out color2);

        Color color3;
        ColorUtility.TryParseHtmlString("#".ToString() + "9494FF", out color3);

        arms.color = color;
        abs.color = color;
        legs.color = color;
        buttocks1.color = color;
        buttocks2.color = color;

        if (user.PreferPart == "Arms")
        {
            arms.color = color2;
        }
        else if (user.PreferPart == "Abs")
        {
            abs.color = color2;
        }
        else if (user.PreferPart == "Legs")
        {
            legs.color = color2;
        }
        else if (user.PreferPart == "Buttocks")
        {
            buttocks1.color = color2;
            buttocks2.color = color3;
        }
        else
        {
            arms.color = color2;
            abs.color = color2;
            legs.color = color2;
            buttocks1.color = color2;
            buttocks2.color = color3;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
