using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPageSetUp : MonoBehaviour
{
    DBUtils dBUtils;

    public GameObject girl;
    public GameObject boy;

    public RawImage hair_girl;
    public RawImage body_girl;
    public RawImage cloth_girl;

    public RawImage hair_boy;
    public RawImage body_boy;
    public RawImage cloth_boy;

    public TextMeshProUGUI playerName;

    public GameObject UserPage;
    public GameObject PlanPage;
    public GameObject TrainPage;

    public int nowState = 0;

    User user;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();

        user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        if (user.Gender == "Girl")
        {
            girl.SetActive(true);
            boy.SetActive(false);
        }
        else
        {
            girl.SetActive(false);
            boy.SetActive(true);
        }
        UserSetUp();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState()
    {
        if (nowState == 0)
        {
            UserPage.SetActive(true);
            PlanPage.SetActive(false);
            TrainPage.SetActive(false);
        }
        else if (nowState == 1)
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

    void UserSetUp()
    {
        playerName.text = user.Name;
        if (user.Gender == "Girl")
        {
            girl.SetActive(true);
            boy.SetActive(false);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Hair, out color);
            hair_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Body, out color);
            body_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Cloth, out color);
            cloth_girl.color = color;
        }
        else
        {
            girl.SetActive(false);
            boy.SetActive(true);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Hair, out color);
            hair_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Body, out color);
            body_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Cloth, out color);
            cloth_boy.color = color;
        }

    }



}
