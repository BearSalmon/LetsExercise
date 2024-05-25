using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class SelectPlayer : MonoBehaviour
{
    DBUtils dBUtils;

    public GameObject girl;
    public GameObject boy;

    public RawImage hair_girl;
    public RawImage body_girl;
    public RawImage cloth_girl;

    public RawImage hair_boy;
    public RawImage body_boy;
    public RawImage cloth_boy;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI lastLogin;

    int nowSelect = 1;
    int number;
    User user;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        number = dBUtils.CountUsers();
        updatePlayer(nowSelect);

        GenerateAllUserRecommendation();
    }

    public void nextOption()
    {
        nowSelect++;
        if (nowSelect > number)
        {
            nowSelect = 1;
        }
        updatePlayer(nowSelect);
    }

    void updatePlayer(int nowSelect)
    {
        user = dBUtils.GetUserByName("User"+nowSelect.ToString());
        playerName.text = user.Name;
        lastLogin.text = user.LastLogin;

        if (user.Gender == "Girl")
        {
            girl.SetActive(true);
            boy.SetActive(false);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString()+ user.Hair, out color);
            hair_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Body, out color);
            body_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Cloth, out color);
            cloth_girl.color = color;
        }
        else
        {
            girl.SetActive(false);
            boy.SetActive(true);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Hair, out color);
            hair_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Body, out color);
            body_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Cloth, out color);
            cloth_boy.color = color;
        }

    }

    public void Save()
    {
        dBUtils.nowPlayer = "User" + nowSelect.ToString();
    }


    void GenerateAllUserRecommendation()
    {
        int userNum = dBUtils.CountUsers();
        for (int i = 1; i <= userNum; i++)
        {
            User user = dBUtils.GetUserByName("User" + i);
            if (user.HasUnfinishedPlan == false)
            {
                string[] recommendValues;
                recommendValues = user.Recommendation.TrimEnd(',').Split(',');
                int[] recommendValuesInt = Array.ConvertAll(recommendValues, int.Parse);

                int maxIndex_0_4 = -1;
                int maxValue_0_4 = -1;

                for (int j = 0; j < 5; j++)
                {
                    if (recommendValuesInt[j] > maxValue_0_4)
                    {
                        maxValue_0_4 = recommendValuesInt[j];
                        maxIndex_0_4 = j;
                    }
                }

                int maxIndex_5_7 = -1;
                int maxValue_5_7 = -1;

                for (int j = 5; j < 8; j++)
                {
                    if (recommendValuesInt[j] > maxValue_5_7)
                    {
                        maxValue_5_7 = recommendValuesInt[j];
                        maxIndex_5_7 = j;
                    }
                }

                string part = GetRecommendLabel(maxIndex_0_4);
                string level = GetRecommendLabel(maxIndex_5_7);
 
                ////////  wait to be update 
                user.RecommendationPoseSet = "";
                IEnumerable<Pose> poses;
                poses = dBUtils.GetPoseByPart("Arms");
                IEnumerable<string> poseNames = poses.Select(p => p.Name);
                List<string> poseNameList = poseNames.ToList();
                foreach (string name in poseNameList)
                {
                    user.RecommendationPoseSet += name + ',';
                }
                /////// end 
                
                user.Level = level;
                user.PreferPart = part;
                user.HasUnfinishedPlan = true;
                dBUtils.UpdateUser(user);
            }
        }
    }

    string GetRecommendLabel(int index)
    {
        if (index == 0)
        {
            return "Arms";
        }
        else if (index == 1)
        {
            return "Abs";
        }
        else if (index == 2)
        {
            return "Buttocks";
        }
        else if (index == 3)
        {
            return "Legs";
        }
        else if (index == 4)
        {
            return "Whole Body";
        }
        else if (index == 5)
        {
            return "Easy";
        }
        else if (index == 6)
        {
            return "Medium";
        }
        else
        {
            return "Hard";
        }
    }
}
