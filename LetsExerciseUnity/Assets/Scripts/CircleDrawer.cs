using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CircleDrawer : MonoBehaviour
{

    public Image circleImage;

    private bool isIncreasing = false;
    private Coroutine increaseCoroutine;

    public ButtonEvent buttonEvent;
    Scene m_Scene;
    Scene f_Scene;

    // Start is called before the first frame update
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        f_Scene = SceneManager.GetActiveScene();
        circleImage.fillAmount = 0f;
    }

    void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        if (m_Scene.buildIndex != f_Scene.buildIndex)
        {
         
            buttonEvent = GetComponent<ButtonEvent>();
       
            if (m_Scene.name != "SampleScene")
            {
                StopIncreasing();
                circleImage = GameObject.Find("Circle").GetComponent<Image>();
                circleImage.fillAmount = 0f;
            }
        }
        f_Scene = SceneManager.GetActiveScene();

    }

    public void CallDrawer()
    {
        if (!isIncreasing) 
        {
            if (m_Scene.name == "InquireData")
            {
                increaseCoroutine = StartCoroutine(IncreaseProgressOverTime(1f));
            }
            else
            {
                increaseCoroutine = StartCoroutine(IncreaseProgressOverTime(2f));
            }
            
        }

    }

    public void StopIncreasing()
    {
        if (isIncreasing && increaseCoroutine != null) // Check if increasing and coroutine is running
        {
            StopCoroutine(increaseCoroutine); // Stop the coroutine

            circleImage = GameObject.Find("Circle").GetComponent<Image>();
            circleImage.fillAmount = 0f;
            isIncreasing = false; // Reset the flag
         
        }
    }

    IEnumerator IncreaseProgressOverTime(float duration)
    {
        float timer = 0f;
        float startProgress = circleImage.fillAmount;
        float endProgress = 1f;

        isIncreasing = true;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            circleImage.fillAmount = Mathf.Lerp(startProgress, endProgress, timer / duration);
            yield return null; // Wait for the next frame
        }

        circleImage.fillAmount = endProgress;
        // click
        buttonEvent.ButtonClick(buttonEvent.currentClickingButton);
        isIncreasing = false;
    }
}

