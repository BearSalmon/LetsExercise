using System.Collections;
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

    IEnumerable<Pose> poses;

    void Start()
    {

        Exercise.SetActive(false);
        Ready.SetActive(true);
        nowState = 1;
        animationCode = GetComponent<AnimationCode>();
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        countDownTimer = GetComponent<CountDownTimer>();
        readyPageUi = GetComponent<ReadyPageUi>();
        exercisePageUI = GetComponent<ExercisePageUI>();

        nowPose = 1;
        poses = dBUtils.GetPoseByPart("arms");
        poseSetCount = poses.Count();
        SetUpPath();
        readyPageUi.SetUp(poses.Skip(nowPose - 1).FirstOrDefault().Name, nowPose, poseSetCount, "test123");
        //countDownTimer.StartCountDown(5f);
       
    }

    public void SetUpPoseSet()
    {
        // plan
        if (buttonEvent.planOrTrain == 0)
        {

        }
        else
        {

        }
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
            animationCode.setBodyList();
            Ready.SetActive(false);


            exercisePageUI.SetUp(poses.Skip(nowPose - 1).FirstOrDefault().Name);

            exercisePageUI.CallDrawer();
            countDownTimer.StartCountDown(10f);

        }
        else
        {
            nowState = 1;
            Exercise.SetActive(false);
            Ready.SetActive(true);
            animationCode.setBodyList();

            nowPose += 1;
            if (nowPose > poseSetCount)
            {
                SceneManager.LoadScene(11);
            }
            else
            {
                SetUpPath();
                readyPageUi.SetUp(poses.Skip(nowPose - 1).FirstOrDefault().Name, nowPose, poseSetCount, "test123");

                countDownTimer.StartCountDown(5f);
            } 
        }
    }

    void Update()
    {
        
    }
}
