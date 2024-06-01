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

    // alignment
    public Image alignment;
    public Image alignment_check;
    private Coroutine increaseCoroutine;
    private bool isIncreasing = false;


    void Start()
    {
        udpReceive = GameObject.Find("WholeManager").GetComponent<UDPReceive>();
        //alignment.fillAmount = 0f;
        alignment.enabled = false;
        isIncreasing = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (exercise.activeSelf == true && !isProcessing)
        {
            wrong_message.text = udpReceive.dataAngle;
            // pos_message.text = udpReceive.dataPos;
            if (udpReceive.dataWrongPart != "fuck")
            {
                wrongPart = udpReceive.dataWrongPart.TrimEnd(',').Split(",");
                StartCoroutine(ProcessWrongParts());
            }
        }

        if (pos_message.text != "")
        {
            StopIncreasing();
            alignment.enabled = true;
        }

        if (pos_message.text == "")
        {
            CallDrawer();
        }
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


    public void StopIncreasing()
    {
        if (isIncreasing && increaseCoroutine != null) // Check if increasing and coroutine is running
        {
            StopCoroutine(increaseCoroutine); // Stop the coroutine
            alignment_check.fillAmount = 0f;
            isIncreasing = false; // Reset the flag

        }
    }

    public void CallDrawer()
    {
        if (!isIncreasing)
        {
            increaseCoroutine = StartCoroutine(IncreaseProgressOverTime(3f));
        }
    }

    IEnumerator IncreaseProgressOverTime(float duration)
    {
        float timer = 0f;
        float startProgress = alignment_check.fillAmount;
        float endProgress = 1f;

        isIncreasing = true;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            alignment_check.fillAmount = Mathf.Lerp(startProgress, endProgress, timer / duration);
            yield return null; // Wait for the next frame
        }

        alignment_check.fillAmount = endProgress;

        alignment_check.fillAmount = 0f;
        isIncreasing = false;

    }
}
