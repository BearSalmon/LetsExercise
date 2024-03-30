using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadyPageUi : MonoBehaviour
{

    public TextMeshProUGUI poseName;
    public TextMeshProUGUI poseCount;
    public TextMeshProUGUI poseSetName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(string poseName , int nowCount, int allCount , string poseSetName)
    {
        this.poseName.text = poseName;
        poseCount.text = nowCount.ToString() + "/" + allCount.ToString() + " part of";
        this.poseSetName.text = poseSetName;
    }
}
