using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CalendarUI : MonoBehaviour
{
    public class Day
    {
        public int dayNum;
        public Color dayColor;
        public Button obj;

        public Day(int dayNum , Color dayColor , Button obj)
        {
            this.dayNum = dayNum;
            this.dayColor = dayColor;
            this.obj = obj;
            obj.GetComponent<Image>().color = dayColor;

        }

        public void UpdateColor(Color newColor)
        {
            obj.GetComponent<Image>().color = newColor;
            dayColor = newColor;
        }

        public void UpdateDay(int newDayNum)
        {
            this.dayNum = newDayNum;
            if (dayColor == Color.white || dayColor == Color.green)
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().text = (dayNum + 1).ToString();
            }
            else
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private List<Day> days = new List<Day>();
    public Button[] weeks;
    public TextMeshProUGUI Month;
    public DateTime currDate = DateTime.Now;


    void Start()
    {
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);
    }
    

    public void UpdateCalendar(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);
        currDate = temp;
        Month.text = temp.Month.ToString() + " " + temp.Year.ToString();
        int startDay = GetMonthStartDay(year, month);
        int endDay = GetTotalNumberOfDays(year, month);


        ///Create the days
        ///This only happens for our first Update Calendar when we have no Day objects therefore we must create them

        if (days.Count == 0)
        {
            for (int w = 0; w < 6; w++)
            {
                for (int i = 0; i < 7; i++)
                {
                    Day newDay;
                    int currDay = (w * 7) + i;
                    if (currDay < startDay || currDay - startDay >= endDay)
                    {
                        newDay = new Day(currDay - startDay, Color.grey, weeks[w * 7 + i]);
                    }
                    else
                    {
                        newDay = new Day(currDay - startDay, Color.white, weeks[w * 7 + i]);
                        
                    }
                    days.Add(newDay);
                    days[w * 7 + i].UpdateDay(w * 7 + i - startDay);
                }
            }
        }
        ///loop through days
        ///Since we already have the days objects, we can just update them rather than creating new ones
        else
        {
            for (int i = 0; i < 42; i++)
            {
                if (i < startDay || i - startDay >= endDay)
                {
                    days[i].UpdateColor(Color.grey);
                }
                else
                {
                    days[i].UpdateColor(Color.white);
                }

                days[i].UpdateDay(i - startDay);
            }
        }

        ///This just checks if today is on our calendar. If so, we highlight it in green
        if (DateTime.Now.Year == year && DateTime.Now.Month == month)
        {
            days[(DateTime.Now.Day - 1) + startDay].UpdateColor(Color.green);
        }

    }

    int GetMonthStartDay(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);

        //DayOfWeek Sunday == 0, Saturday == 6 etc.
        return (int)temp.DayOfWeek;
    }

    int GetTotalNumberOfDays(int year, int month)
    {
        return DateTime.DaysInMonth(year, month);
    }

    public void SwitchMonth(int direction)
    {
        if (direction < 0)
        {
            currDate = currDate.AddMonths(-1);
        }
        else
        {
            currDate = currDate.AddMonths(1);
        }

        UpdateCalendar(currDate.Year, currDate.Month);
    }


}
