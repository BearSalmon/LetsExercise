using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanPageUI : MonoBehaviour
{
    // Start is called before the first frame update
    DBUtils dBUtils;
    ButtonEvent buttonEvent;

    public GameObject Pass;
    public GameObject UnPass;

    User user;

    void Start()
    {
        
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        setPlanPassOrNot();
    }

    void setPlanPassOrNot()
    {
        if (user.HasUnfinishedPlan == true)
        {
            Pass.SetActive(false);
            UnPass.SetActive(true);
        }
        else
        {
            Pass.SetActive(true);
            UnPass.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
