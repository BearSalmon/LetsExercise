using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPageSetUp : MonoBehaviour
{

    public GameObject UserPage;
    public GameObject PlanPage;
    public GameObject TrainPage;

    public bool isOpening;
    public GameObject Menu;

    public int nowState;

    public Button menuBtn1;
    public Button menuBtn2;
    public Button menuBtn3;
    public Button menuBtn4;

    DBUtils dBUtils;
    Record record;
    DateTime currDate = DateTime.Now;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();

        nowState = 0;
        ChangeState(nowState);

        Menu.SetActive(false);
        isOpening = false;
        addCalendarRecord();
    }

    public void addCalendarRecord()
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
        if (record == null)
        {
            dBUtils.AddRecord(dBUtils.nowPlayer, serchTerm);
        }
    }

    public void ChangeState(int state)
    {
        nowState = state;
        if (state == 0)
        {
            UserPage.SetActive(true);
            PlanPage.SetActive(false);
            TrainPage.SetActive(false);
            menuBtn1.name = "Btn7";
            menuBtn2.name = "Btn8";
            menuBtn3.name = "Btn9";
            menuBtn4.name = "Btn10";
        }
        else if (state == 1)
        {
            UserPage.SetActive(false);
            PlanPage.SetActive(true);
            TrainPage.SetActive(false);
            menuBtn1.name = "Btn7";
            menuBtn2.name = "Btn8";
            menuBtn3.name = "Btn9";
            menuBtn4.name = "Btn10";
        }
        else if (state == 2)
        {
            UserPage.SetActive(false);
            PlanPage.SetActive(false);
            TrainPage.SetActive(true);
            menuBtn1.name = "Btn8";
            menuBtn2.name = "Btn9";
            menuBtn3.name = "Btn10";
            menuBtn4.name = "Btn11";

        }

    }
    public void SetMenu()
    {
        if (isOpening == false)
        {
            Menu.SetActive(true);
            isOpening = true;
        }
        else
        {
            Menu.SetActive(false);
            isOpening = false;
        }
    }





}
