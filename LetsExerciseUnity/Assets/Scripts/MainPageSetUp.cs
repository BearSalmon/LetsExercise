using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainPageSetUp : MonoBehaviour
{

    public GameObject UserPage;
    public GameObject PlanPage;
    public GameObject TrainPage;

    public bool isOpening;
    public GameObject Menu;

    public int nowState;

    public Button menuBtn1;
    public Button menuBtn2;
    public Button menuBtn3;


    // Start is called before the first frame update
    void Start()
    {
        nowState = 0;
        ChangeState(nowState);

        Menu.SetActive(false);
        isOpening = false;
    }

    public void ChangeState(int state)
    {
        nowState = state;
        if (state == 0)
        {
            UserPage.SetActive(true);
            PlanPage.SetActive(false);
            TrainPage.SetActive(false);
            menuBtn1.name = "Btn6";
            menuBtn2.name = "Btn7";
            menuBtn3.name = "Btn8";
        }
        else if (state == 1)
        {
            UserPage.SetActive(false);
            PlanPage.SetActive(true);
            TrainPage.SetActive(false);
            menuBtn1.name = "Btn5";
            menuBtn2.name = "Btn6";
            menuBtn3.name = "Btn7";
        }
        else if (state == 2)
        {
            UserPage.SetActive(false);
            PlanPage.SetActive(false);
            TrainPage.SetActive(true);
            menuBtn1.name = "Btn8";
            menuBtn2.name = "Btn9";
            menuBtn3.name = "Btn10";

        }

    }
    public void SetMenu()
    {
        if (isOpening == false)
        {
            Menu.SetActive(true);
            isOpening = true;
            if (nowState == 0)
            {
                menuBtn1.name = "Btn6";
                menuBtn2.name = "Btn7";
                menuBtn3.name = "Btn8";
            }
            else if (nowState == 1)
            {
                menuBtn1.name = "Btn5";
                menuBtn2.name = "Btn6";
                menuBtn3.name = "Btn7";
            }
            else
            {
                menuBtn1.name = "Btn8";
                menuBtn2.name = "Btn9";
                menuBtn3.name = "Btn10";
            }

        }
        else
        {
            Menu.SetActive(false);
            isOpening = false;
        }
    }





}
