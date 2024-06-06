using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System;

public class WholeSampleSceneManager : MonoBehaviour
{
    // ready or exercise
    // ready =1
    // exercise =0
    public int nowState;

    public int nowPose;

    public int poseSetCount;

    public DBUtils dBUtils;
    public CountDownTimer countDownTimer;
    public AnimationCode animationCode;
    public ReadyPageUi readyPageUi;
    public ExercisePageUI exercisePageUI;
    ButtonEvent buttonEvent;

    public GameObject Exercise;
    public GameObject Ready;
    public NormalizeBody normalizeBody;

    public UDPSend udpsend;
    public UDPReceive udpreceive;

    public bool isAnimating;
    public bool isCounting;

    User user;
    public List<Pose> poses = new List<Pose>();

    public TextMeshProUGUI countDownMessage;

    PoseSet poseSet;

    string level;

    void Start()
    {
        isAnimating = false;
        isCounting = false;
        countDownMessage.text = "";
        Exercise.SetActive(false);
        Ready.SetActive(true);
        nowState = 1;
        animationCode = GetComponent<AnimationCode>();
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        countDownTimer = GetComponent<CountDownTimer>();
        readyPageUi = GetComponent<ReadyPageUi>();
        exercisePageUI = GetComponent<ExercisePageUI>();

        udpsend = GameObject.Find("WholeManager").GetComponent<UDPSend>();
        udpreceive = GameObject.Find("WholeManager").GetComponent<UDPReceive>();

        if (buttonEvent.planOrTrain == 0)
        {
            level = user.Level;
        }
        else
        {
            level = buttonEvent.poseSetLevel;
        }


        SetUpPoseSet();
        nowPose = 0;
        SetUpPath();

        // plan
        if (buttonEvent.planOrTrain == 0)
        {
            readyPageUi.SetUp(poses[nowPose].Name, nowPose + 1, poseSetCount, "Daily Exercise Menu");
        }
        // train
        else {
            readyPageUi.SetUp(poses[nowPose].Name, nowPose + 1, poseSetCount, poseSet.PoseSetName);
        }
        countDownTimer.StartCountDown(5f);
    }

    public void SetUpPoseSet()
    {
        poses.Clear();
        string[] poseNames;
        // plan
        if (buttonEvent.planOrTrain == 0)
        {
            poseNames = user.RecommendationPoseSet.TrimEnd(',').Split(',');
        }
        // train
        else
        {
            poseSet = dBUtils.GetPoseSetById(buttonEvent.poseSetID);
            poseNames = poseSet.TrainPoseSet.TrimEnd(',').Split(',');
        }
        foreach (string name in poseNames)
        {
            Pose pose = dBUtils.GetPoseByName(name);
            poses.Add(pose);
        }

        if (buttonEvent.planOrTrain == 1)
        {
            poses = poses.OrderBy(x => Guid.NewGuid()).ToList();
        }
        poseSetCount = poses.Count();
    }

    public void SetUpPath()
    {
        string path = poses[nowPose].Path;
        animationCode.ChangeLineList(path);
        udpsend.SendDataForPoseset(path);
    }

    public void StartAnimation()
    {
        isAnimating = true;
        udpsend.SendDataForCounter("start");
    }

    public void StopAnimation()
    {
        isAnimating = false;
        exercisePageUI.wrong_message.text = "nice";
    }

    IEnumerator StartCountdownWithDelay()
    {
        isCounting = true;
        for (int i = 3; i > 0; i--)
        {
            countDownMessage.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDownMessage.text = "";
        isCounting = false;
        Exercise.SetActive(true);

        if (level == "Easy")
        {
            countDownTimer.StartCountDown(10f);
        }
        else if (level == "Medium")
        {
            countDownTimer.StartCountDown(10f);
        }
        else
        {
            countDownTimer.StartCountDown(15f);
        }
    }


    public void ChangeView()
    {
        if (nowState == 1)
        {
            nowState = 0;
            animationCode.counter = 0;
            Ready.SetActive(false);

            exercisePageUI.SetUp(poses[nowPose].Name);

            StartCoroutine(StartCountdownWithDelay());

            

        }
        else
        {
            nowState = 1;
            Exercise.SetActive(false);
            Ready.SetActive(true);
            nowPose += 1;
            if (nowPose == poseSetCount)
            {
                SceneManager.LoadScene((int)ButtonEvent.SceneName.MainPage);
            }
            else
            {
                SetUpPath();      
                // plan
                if (buttonEvent.planOrTrain == 0)
                {
                    readyPageUi.SetUp(poses[nowPose].Name, nowPose + 1, poseSetCount, "Daily Exercise Menu");
                }
                // train
                else
                {
                    readyPageUi.SetUp(poses[nowPose].Name, nowPose + 1, poseSetCount, poseSet.PoseSetName);
                }

                countDownTimer.StartCountDown(5f);
                //normalizeBody.changeBodyScale();
            } 
        }
    }

}
