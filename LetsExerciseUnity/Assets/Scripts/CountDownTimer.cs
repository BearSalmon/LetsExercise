using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float currentTime = -1f;
    [SerializeField] float startingTime = 30f;
    [SerializeField] TextMeshProUGUI count;
    public Image progressBar;


    public AnimationCode animationCode;

    // Start is called before the first frame update
    void Start()
    {
        count.text = startingTime.ToString();


        animationCode = GameObject.Find("Manager").GetComponent<AnimationCode>();

        progressBar.fillAmount = 0f;

    }

    public void StartCountDown()
    {

        currentTime = startingTime;
        progressBar.fillAmount = 0f;

        animationCode.StartAnimation();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime < 10)
            {
                count.text = " "+ Mathf.RoundToInt(currentTime).ToString();
            }
            else
            {
                count.text = Mathf.RoundToInt(currentTime).ToString();
            }
            
            progressBar.fillAmount += 1* Time.deltaTime / startingTime;
            
        }
        else if (currentTime == 0)
        {
            count.text = startingTime.ToString(); // Or any message you want when the countdown reaches zero
            currentTime = -1;
            animationCode.StopAnimation();
        }
    }
}
