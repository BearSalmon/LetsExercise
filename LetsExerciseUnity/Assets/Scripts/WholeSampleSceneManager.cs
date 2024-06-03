using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

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

    User user;
    public List<Pose> poses = new List<Pose>();

    PoseSet poseSet;

    string level;

    void Start()
    {
        isAnimating = false;
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

        // normalizeBody.changeBodyScale();

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
        //normalizeBody.changeBodyScale();
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
            Debug.Log(name);
            Pose pose = dBUtils.GetPoseByName(name);
            poses.Add(pose);
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
    }

    public void ChangeView()
    {
        if (nowState == 1)
        {
            nowState = 0;
            animationCode.counter = 0;
            Exercise.SetActive(true);
            Ready.SetActive(false);

            exercisePageUI.SetUp(poses[nowPose].Name);

            exercisePageUI.CallDrawer();

            if (level == "Easy")
            {
                countDownTimer.StartCountDown(20f);
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
        else
        {
            nowState = 1;
            Exercise.SetActive(false);
            Ready.SetActive(true);
            // normalizeBody.changeBodyScale();
            nowPose += 1;
            // normalizeBody.changeBodyScale();
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
