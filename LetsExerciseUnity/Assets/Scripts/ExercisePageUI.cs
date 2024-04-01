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

    string [] wrongPart;

    private bool isProcessing = false;

    // alignment
    public Image alignment;
    private Coroutine increaseCoroutine;
    private bool isIncreasing = false;


    void Start()
    {
        udpReceive = GameObject.Find("Manager").GetComponent<UDPReceive>();
        alignment.fillAmount = 0f;
        isIncreasing = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (exercise.activeSelf == true && !isProcessing)
        {
            wrong_message.text = udpReceive.dataAngle;
            pos_message.text = udpReceive.dataPos;
            wrongPart = udpReceive.dataWrongPart.Split(",");

            StartCoroutine(ProcessWrongParts());
        }

        if (pos_message.text != "")
        {
            StopIncreasing();
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
            alignment.fillAmount = 0f;
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
        float startProgress = alignment.fillAmount;
        float endProgress = 1f;

        isIncreasing = true;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            alignment.fillAmount = Mathf.Lerp(startProgress, endProgress, timer / duration);
            yield return null; // Wait for the next frame
        }

        alignment.fillAmount = endProgress;

        alignment.fillAmount = 0f;
        isIncreasing = false;

    }
}
