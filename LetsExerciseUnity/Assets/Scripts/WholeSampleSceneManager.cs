using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public GameObject Exercise;
    public GameObject Ready;

    IEnumerable<Pose> poses;

    void Start()
    {
        nowState = 1;
        animationCode = GameObject.Find("Manager").GetComponent<AnimationCode>();
        dBUtils = GameObject.Find("Manager").GetComponent<DBUtils>();
        countDownTimer = GetComponent<CountDownTimer>();
        readyPageUi = GetComponent<ReadyPageUi>();

        nowPose = 1;
        poses = dBUtils.GetPoseByPart("arms");
        poseSetCount = poses.Count();
        SetUpPath();
        readyPageUi.SetUp(poses.Skip(nowPose - 1).FirstOrDefault().Name, nowPose, poseSetCount, "test123");
        //countDownTimer.StartCountDown(5f);
    }

    public void SetUpPath()
    {
        string path = poses.Skip(nowPose - 1).FirstOrDefault().Path;
        animationCode.ChangeLineList(path);
    }

    public void ChangeView()
    {
        if (nowState == 1)
        {
            nowState = 0;
            Exercise.SetActive(true);
            Ready.SetActive(false);
            countDownTimer.StartCountDown(5f);
        }
        else
        {
            nowState = 1;
            Exercise.SetActive(false);
            Ready.SetActive(true);

            nowPose += 1;
            SetUpPath();
            readyPageUi.SetUp(poses.Skip(nowPose - 1).FirstOrDefault().Name, nowPose, poseSetCount, "test123");

            countDownTimer.StartCountDown(5f);
        }
    }

    void Update()
    {
        
    }
}
