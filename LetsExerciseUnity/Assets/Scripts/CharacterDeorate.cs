using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterDeorate : MonoBehaviour
{
    public int state = 0;
    public GameObject palette_hair;
    public GameObject palette_body;
    public GameObject palette_cloth;
    public GameObject girl;
    public GameObject boy;

    public RawImage hair_girl;
    public RawImage body_girl;
    public RawImage cloth_girl;

    public RawImage hair_boy;
    public RawImage body_boy;
    public RawImage cloth_boy;

    public DBUtils dBUtils;


    User user;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();

        user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        UserSetUp();
    }

    void UserSetUp()
    {
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

    public void changeState()
    {
        if (state == 1)
        {
            palette_hair.SetActive(false);
            palette_body.SetActive(true);
        }
        else if (state == 2)
        {
            palette_body.SetActive(false);
            palette_cloth.SetActive(true);
        }
    }

    public void changeColor(Color color)
    {
        if (user.Gender == "Girl")
        {
            if (state == 0)
            {
                hair_girl.color = color;
            }
            else if (state == 1)
            {
                body_girl.color = color;
            }
            else
            {
                cloth_girl.color = color;
            }

        }
        else 
        {
            if (state == 0)
            {
                hair_boy.color = color;
            }
            else if (state == 1)
            {
                body_boy.color = color;
            }
            else
            {
                cloth_boy.color = color;
            }

        }
        

    }
}
