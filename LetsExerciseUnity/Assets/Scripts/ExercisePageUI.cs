using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExercisePageUI : MonoBehaviour
{
    // Start is called before the first frame update


    public TextMeshProUGUI wrong_message;
    public TextMeshProUGUI pos_message;
    public TextMeshProUGUI poseName;
    public UDPReceive udpReceive;
    public GameObject exercise;

    public LineRenderer rightUpperArm;
    public LineRenderer leftUpperArm;
    public LineRenderer rightForeArm;
    public LineRenderer leftForeArm;
    public LineRenderer rightThigh;
    public LineRenderer leftThigh;
    public LineRenderer rightCalf;
    public LineRenderer leftCalf;

    string [] wrongPart;

    private bool isProcessing = false;
    WholeSampleSceneManager wholeSampleSceneManager;

    bool canGetWrongMessage;


    void Start()
    {
        wholeSampleSceneManager = GetComponent<WholeSampleSceneManager>();
        udpReceive = GameObject.Find("WholeManager").GetComponent<UDPReceive>();
        wrong_message.text = "nice";
        canGetWrongMessage = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (exercise.activeSelf == true && !isProcessing && wholeSampleSceneManager.isAnimating)
        {
            if (canGetWrongMessage)
            {
                StartCoroutine(UpdateWrongMessageWithDelay());
            }
            wrongPart = udpReceive.dataWrongPart.TrimEnd(',').Split(",");
            StartCoroutine(ProcessWrongParts());
        }

    }

    private IEnumerator UpdateWrongMessageWithDelay()
    {
        canGetWrongMessage = false;
        yield return new WaitForSeconds(3f); // Wait for 5 seconds
        canGetWrongMessage = true;
        wrong_message.text = udpReceive.dataAngle;
    }

    private IEnumerator ProcessWrongParts()
    {
        isProcessing = true;

        foreach (string part in wrongPart)
        {
            if (part == "right fore arm")
            {
                SetLineWrongColor(rightForeArm);
            }
            else if (part == "left fore arm")
            {
                SetLineWrongColor(leftForeArm);
            }
            else if (part == "right upper arm")
            {
                SetLineWrongColor(rightUpperArm);
            }
            else if (part == "left upper arm")
            {
                SetLineWrongColor(leftUpperArm);
            }
            else if (part == "right thigh")
            {
                SetLineWrongColor(rightThigh);
            }
            else if (part == "left thigh")
            {
                SetLineWrongColor(leftThigh);
            }
            else if (part == "right calf")
            {
                SetLineWrongColor(rightCalf);
            }
            else if (part == "left calf")
            {
                SetLineWrongColor(leftCalf);
            }
        }

        yield return new WaitForSeconds(1f); // Add a one-second time gap

        foreach (string part in wrongPart)
        {
            if (part == "right fore arm")
            {
                SetLineCorrectColor(rightForeArm);
            }
            else if (part == "left fore arm")
            {
                SetLineCorrectColor(leftForeArm);
            }
            else if (part == "right upper arm")
            {
                SetLineCorrectColor(rightUpperArm);
            }
            else if (part == "left upper arm")
            {
                SetLineCorrectColor(leftUpperArm);
            }
            else if (part == "right thigh")
            {
                SetLineCorrectColor(rightThigh);
            }
            else if (part == "left thigh")
            {
                SetLineCorrectColor(leftThigh);
            }
            else if (part == "right calf")
            {
                SetLineCorrectColor(rightCalf);
            }
            else if (part == "left calf")
            {
                SetLineCorrectColor(leftCalf);
            }
        }

        isProcessing = false;
    }


    void SetLineWrongColor(LineRenderer line )
    {
        line.startColor = Color.red;
        line.endColor = Color.red;
    }

    void SetLineCorrectColor(LineRenderer line)
    {
        line.startColor = Color.white;
        line.endColor = Color.white;
    }

    public void SetUp(string poseName)
    {
        this.poseName.text = poseName;
    }



   
}
