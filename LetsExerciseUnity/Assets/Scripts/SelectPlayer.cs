using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectPlayer : MonoBehaviour
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
    public TextMeshProUGUI lastLogin;

    int nowSelect = 1;
    int number;
    User user;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        number = dBUtils.CountUsers();
        updatePlayer(nowSelect);
    }

    public void nextOption()
    {
        nowSelect++;
        if (nowSelect > number)
        {
            nowSelect = 1;
        }
        updatePlayer(nowSelect);
    }

    public void backOption()
    {
        nowSelect--;
        if (nowSelect < 0)
        {
            nowSelect = number;
        }
        updatePlayer(nowSelect);

    }

    void updatePlayer(int nowSelect)
    {
        user = dBUtils.GetUserByName("User"+nowSelect.ToString());
        playerName.text = user.Name;
        lastLogin.text = user.LastLogin;

        if (user.Gender == "Girl")
        {
            girl.SetActive(true);
            boy.SetActive(false);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString()+ user.Hair, out color);
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

    public void Save()
    {
        dBUtils.nowPlayer = "User" + nowSelect.ToString();
    }
}
