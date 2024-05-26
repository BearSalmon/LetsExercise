using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameUI : MonoBehaviour
{
    // Start is called before the first frame update

    DBUtils dBUtils;
    Record record;
    DateTime currDate = DateTime.Now;

    User user;

    public GameObject Coach;
    public GameObject Level;
    public GameObject PreferPart;
    public GameObject Mood;
    public GameObject Bye;

    public int nowState = 0;

    string recommendation = "";

    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        Coach.SetActive(true);
        Level.SetActive(false);
        PreferPart.SetActive(false);
        Mood.SetActive(false);
        Bye.SetActive(false);
        
    }

    public void updateMood(string mood)
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
        record.Mood = mood;
        dBUtils.UpdateRecord(record);
    }

    public void ChangeState(int state)
    {
        nowState = state;
        if (state == 1)
        {
            Coach.SetActive(false);
            Level.SetActive(true);
        }
        else if (state == 2)
        {
            Level.SetActive(false);
            PreferPart.SetActive(true);
        }
        else if (state == 3)
        {
            PreferPart.SetActive(false);
            Mood.SetActive(true);

        }
        else if (state == 4)
        {
            Mood.SetActive(false);
            Bye?.SetActive(true);
        }

    }
    public string SetRecommandation(string input)
    {
        recommendation = "";
        string[] recommendValues;
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        recommendValues = user.Recommendation.TrimEnd(',').Split(',');

        if (input == "Easy")
        {
            recommendValues[5] = (int.Parse(recommendValues[5]) + 1).ToString();
        }
        else if (input == "Medium")
        {
            recommendValues[6] = (int.Parse(recommendValues[6]) + 1).ToString();
        }
        else if (input == "Hard")
        {
            recommendValues[7] = (int.Parse(recommendValues[7]) + 1).ToString();
        }
        else if (input == "Arms")
        {
            recommendValues[0] = (int.Parse(recommendValues[0]) + 1).ToString();
        }
        else if (input == "Abs")
        {
            recommendValues[1] = (int.Parse(recommendValues[1]) + 1).ToString();
        }
        else if (input == "Buttocks")
        {
            recommendValues[2] = (int.Parse(recommendValues[2]) + 1).ToString();
        }
        else if (input == "Legs")
        {
            recommendValues[3] = (int.Parse(recommendValues[3]) + 1).ToString();
        }
        else if (input == "Whole Body")
        {
            recommendValues[4] = (int.Parse(recommendValues[4]) + 1).ToString();
        }

        foreach (string part in recommendValues)
        {
            recommendation += part + ",";
        }
        return recommendation;

    }
}
