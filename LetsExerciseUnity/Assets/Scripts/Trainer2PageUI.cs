using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class Trainer2PageUI : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    public Image progressBar1;
    public Image progressBar2;
    public Image progressBar3;
    public Image progressBar4;

    public TextMeshProUGUI T1;
    public TextMeshProUGUI T2;
    public TextMeshProUGUI T3;
    public TextMeshProUGUI T4;

    DBUtils dBUtils;
    User user;

    // Start is called before the first frame update
    void Start()
    {
        progressBar1.fillAmount = 0f;
        progressBar2.fillAmount = 0f;
        progressBar3.fillAmount = 0f;
        progressBar4.fillAmount = 0f;

        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        T1.text = "";
        T2.text = "";
        T3.text = "";
        T4.text = "";

        P1.SetActive(false);
        P2.SetActive(false);
        P3.SetActive(false);
        P4.SetActive(false);
        GenerateUserRecommendation();
        progress1();
    }
    void GenerateUserRecommendation()
    {
        if (user.HasUnfinishedPlan == false)
        {

            string part = user.PreferPart;
            string level = user.Level;

            ////////  wait to be update 
            user.RecommendationPoseSet = "";
            IEnumerable<Pose> poses;
            poses = dBUtils.GetPoseByPart(part);
            IEnumerable<string> poseNames = poses.Select(p => p.Name);
            List<string> poseNameList = poseNames.ToList();
            List<string> randomList = poseNameList.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (string name in randomList)
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

    

    void progress1()
    {

        // 身高體重
        P1.SetActive(true);
        T1.text = "Analyzing data ... ";

        StartCoroutine(IncreaseProgressOverTime(1.5f,progressBar1,1));

    }

    void progress2()
    {
        P2.SetActive(true);
        T2.text = "Calculating metabolism ...  ";
        StartCoroutine(IncreaseProgressOverTime(3f, progressBar2,2));
        
    }

    void progress3()
    {
        P3.SetActive(true);
        T3.text = "Choosing your fitness plan : \n" + user.PreferPart;
        StartCoroutine(IncreaseProgressOverTime(2f, progressBar3,3));
        
    }

    void progress4()
    {
        P4.SetActive(true);
        T4.text = "Select fitness level : \n" + user.Level;
        StartCoroutine(IncreaseProgressOverTime(2.5f, progressBar4,4));
    }

    IEnumerator IncreaseProgressOverTime(float duration , Image progressBar , int now)
    {
        float timer = 0f;
        float startProgress = progressBar.fillAmount;
        float endProgress = 1f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            progressBar.fillAmount = Mathf.Lerp(startProgress, endProgress, timer / duration);
            yield return null; // Wait for the next frame
        }

        progressBar.fillAmount = endProgress;

        if (now == 1)
        {
            progress2();
        }
        else if (now == 2)
        {
            progress3();
        }
        else if (now == 3)
        {
            progress4();
        }
        else
        {
            SceneManager.LoadScene(12);
        }
    }


}
