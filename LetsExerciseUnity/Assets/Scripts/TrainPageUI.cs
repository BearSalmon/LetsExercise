using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class TrainPageUI : MonoBehaviour
{
    public TextMeshProUGUI poseSetName;
    public TextMeshProUGUI calories;
    public TextMeshProUGUI duration;
    public TextMeshProUGUI numberOfGesture;
    public TextMeshProUGUI level;

    public RawImage arms;
    public RawImage abs;
    public RawImage legs;
    public RawImage buttocks1;
    public RawImage buttocks2;

    [SerializeField] Color hard, mid, easy;

    int number;
    int nowSelect = 1;

    DBUtils dBUtils;

    PoseSet poseSet;

    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        number = 3; 
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
        if (nowSelect == 0)
        {
            nowSelect = number;
        }
        updateTrainSet(nowSelect);

        

    }

    public void updateTrainSet(int nowSelect)
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

        poseSet = dBUtils.GetPoseSetById(nowSelect);
        poseSetName.text = poseSet.PoseSetName;
        calories.text = poseSet.Calories.ToString() + " cal";
        duration.text = poseSet.Duration.ToString() + " s";
        numberOfGesture.text = poseSet.NumberOfGesture.ToString();
        level.text = poseSet.Level;

        if (level.text == "Hard")
        {
            level.color = hard;
        }
        else if (level.text == "Mid")
        {
            level.color = mid;
        }
        else
        {
            level.color = easy;
        }

        

        string[] parts = poseSet.Part.Split(" ");

        for (int i=0; i < parts.Length; i++)
        {
            if (parts[i] == "arms")
            {
                arms.color = color2;
            }
            else if (parts[i] == "abs")
            {
                abs.color = color2;
            }
            else if (parts[i] == "legs")
            {
                legs.color = color2;
            }
            else if (parts[i] == "buttocks")
            {
                buttocks1.color = color2;
                buttocks2.color = color2;
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
