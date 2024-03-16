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

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();

        player = dBUtils.GetPlayerByName("Player" + dBUtils.nowPlayer.ToString());
        UserSetUp();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UserSetUp()
    {
        playerName.text = player.Name;
        if (player.Gender == "Girl")
        {
            girl.SetActive(true);
            boy.SetActive(false);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString() + player.Hair, out color);
            hair_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + player.Body, out color);
            body_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + player.Cloth, out color);
            cloth_girl.color = color;
        }
        else
        {
            girl.SetActive(false);
            boy.SetActive(true);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString() + player.Hair, out color);
            hair_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + player.Body, out color);
            body_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + player.Cloth, out color);
            cloth_boy.color = color;
        }

    }

}
