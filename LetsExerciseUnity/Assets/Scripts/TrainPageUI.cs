using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrainPageUI : MonoBehaviour
{
    public TextMeshProUGUI poseSetName;
    public TextMeshProUGUI numberOfGesture;

    public RawImage arms;
    public RawImage abs;
    public RawImage legs;
    public RawImage buttocks1;
    public RawImage buttocks2;

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


    public int nextOption()
    {
        nowSelect++;
        if (nowSelect > number)
        {
            nowSelect = 1;
        }
        updateTrainSet(nowSelect);
        return nowSelect;
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
        numberOfGesture.text = poseSet.NumberOfGesture.ToString();

      
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
