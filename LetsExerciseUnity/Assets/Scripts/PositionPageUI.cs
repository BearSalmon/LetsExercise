using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class PositionPageUI : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI pos_message;
    public TextMeshProUGUI countdown_message;
    public UDPReceive udpReceive;

    // alignment
    public Image alignment;
    public Image alignment_check;
    private Coroutine increaseCoroutine;
    private bool isIncreasing = false;

    public bool loadingCompleted;
    public GameObject playerCamera;
    public GameObject t2;
    public GameObject t3;

    IEnumerator Start()
    {
        pos_message.text = "";
        udpReceive = GameObject.Find("WholeManager").GetComponent<UDPReceive>();
        //alignment.fillAmount = 0f;
        alignment.enabled = false;
        isIncreasing = false;
        yield return new WaitForSeconds(12f);
        playerCamera.SetActive(true);
        t2.SetActive(false);
        t3.SetActive(true);
        loadingCompleted = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (loadingCompleted)
        {
            pos_message.text = udpReceive.dataPos;
            //SceneManager.LoadScene((int)ButtonEvent.SceneName.GameStart);
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
    }

    public void StopIncreasing()
    {
        if (isIncreasing && increaseCoroutine != null) // Check if increasing and coroutine is running
        {
            StopCoroutine(increaseCoroutine); // Stop the coroutine
            alignment_check.fillAmount = 0f;
            countdown_message.text = "";
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

        //alignment_check.fillAmount = endProgress;

        //alignment_check.fillAmount = 0f;
        isIncreasing = false;
        
        if (alignment_check.fillAmount == endProgress)
        {
            countdown_message.text = "3";
            yield return new WaitForSeconds(1f);       
            if (alignment_check.fillAmount == endProgress)
            {
                countdown_message.text = "2";
                yield return new WaitForSeconds(1f);
                if (alignment_check.fillAmount == endProgress)
                {
                    countdown_message.text = "1";
                    yield return new WaitForSeconds(1f);
                    if (alignment_check.fillAmount == endProgress)
                    {
                        SceneManager.LoadScene((int)ButtonEvent.SceneName.GameStart);
                    }
                }
            }
        }
    }
}
