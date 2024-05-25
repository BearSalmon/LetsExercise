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
    public GameObject Detail;
    public TextMeshProUGUI date;
    public TextMeshProUGUI day;
    public TextMeshProUGUI duration;

    DBUtils dBUtils;

    public RawImage arms;
    public RawImage abs;
    public RawImage legs;
    public RawImage buttocks1;
    public RawImage buttocks2;

    public bool detailIsOpen;

    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);
        Detail.SetActive(false);
        detailIsOpen = false;
    }

    int MonthStringToInt(string month)
    {
        switch (month)
        {
            case "January":
                return 1;
            case "February":
                return 2;
            case "March":
                return 3;
            case "April":
                return 4;
            case "May":
                return 5;
            case "June":
                return 6;
            case "July":
                return 7;
            case "August":
                return 8;
            case "September":
                return 9;
            case "October":
                return 10;
            case "November":
                return 11;
            case "December":
                return 12;
            default:
                throw new ArgumentException("Invalid month name");
        }
    }

    string MonthIntToString(int month)
    {
        switch (month)
        {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "June";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";
            default:
                throw new ArgumentException("Invalid month number");
        }
    }


    public void UpdateCalendar(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);
        currDate = temp;
        Month.text = MonthIntToString(temp.Month) + " " + temp.Year.ToString();
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


    public void SetUpDetail(string index)
    {
        if (detailIsOpen == false)
        {
            Detail.SetActive(false);
        }
        else
        {
            Detail.SetActive(true);
            string [] time = Month.text.Split(" ");
            int month = MonthStringToInt(time[0]);
            string serchTerm = "";
            if (month < 10)
            {
                serchTerm = time[1] + "0" + month.ToString();
            }
            else
            {
                serchTerm = time[1] + month.ToString();
            }
            if (index.Length < 2)
            {
                serchTerm += "0" + index;
            }
            else
            {
                serchTerm += index;
            }
            Record record = dBUtils.GetRecordByNameAndDate(dBUtils.nowPlayer, serchTerm);
            updateDetail(record , index , time[0] , time[1]);

        }
    }

    public void updateDetail(Record record , string index , string month , string year)
    {
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

        date.text = index + " " + month + " " + year;

        DateTime dt = new DateTime(int.Parse(year), MonthStringToInt(month), int.Parse(index));
        int dayOfWeek = ((int)dt.DayOfWeek);
        if ( dayOfWeek == 1)
        {
            day.text = "Monday";
        }
        else if (dayOfWeek == 2)
        {
            day.text = "Tuesday";
        }
        else if (dayOfWeek == 3)
        {
            day.text = "Wednesday";
        }
        else if (dayOfWeek == 4)
        {
            day.text = "Thursday";
        }
        else if (dayOfWeek == 5)
        {
            day.text = "Friday";
        }
        else if (dayOfWeek == 6)
        {
            day.text = "Saturday";
        }
        else 
        {
            day.text = "Sunday";
        }


        if (record == null)
        {
            duration.text = "no record";
        }
        else
        {
            duration.text = record.Duration.ToString();

            string[] parts = record.Parts.TrimEnd(',').Split(',');

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "Arms")
                {
                    arms.color = color2;
                }
                else if (parts[i] == "Abs")
                {
                    abs.color = color2;
                }
                else if (parts[i] == "Legs")
                {
                    legs.color = color2;
                }
                else if (parts[i] == "Buttocks")
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

        }
        
    }


}
