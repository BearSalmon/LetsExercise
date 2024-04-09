using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPageSetUp : MonoBehaviour
{
    DBUtils dBUtils;

    public GameObject UserPage;
    public GameObject PlanPage;
    public GameObject TrainPage;
    public int nowState;


    User user;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();

        user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        nowState = 2;
        ChangeState(2);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(int state)
    {
        if (state == 0)
        {
            UserPage.SetActive(true);
            PlanPage.SetActive(false);
            TrainPage.SetActive(false);
        }
        else if (state == 1)
        {
            UserPage.SetActive(false);
            PlanPage.SetActive(true);
            TrainPage.SetActive(false);
        }
        else
        {
            UserPage.SetActive(false);
            PlanPage.SetActive(false);
            TrainPage.SetActive(true);

        }



    }

    



}
