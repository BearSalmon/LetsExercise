using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestigateSet : MonoBehaviour
{
    [SerializeField] ButtonEvent buttonEvent;
    public TextMeshProUGUI bm;
    public TextMeshProUGUI des;
    public TextMeshProUGUI c1;
    public TextMeshProUGUI c2;
    public TextMeshProUGUI c3;

    public DBUtils dBUtils;

    User user;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        if (user.PreferPart == "Arms")
        {
            bm.text = "push-up";
            des.text = "How many moves can you do in one minute?";

        }
        else if (user.PreferPart == "Abs")
        {
            bm.text = "plank";
            des.text = "How many seconds can you hold on before you feel tired?";

        }
        else if (user.PreferPart == "Legs" || user.PreferPart == "Buttocks")
        {
            bm.text = "squat";
            des.text = "How many moves can you do in one minute?";

        }
        else if (user.PreferPart == "Whole Body")
        {
            bm.text = "jumping jacks";
            des.text = "How many moves can you do in one minute?";

        }
        if (user.PreferPart == "Abs")
        {
            c1.text = "less than 30 seconds";
            c2.text = "30 times to 60 seconds";
            c3.text = "more than 60 seconds";
        }
        else
        {
            c1.text = "less than 20 times";
            c2.text = "20 times to 40 times";
            c3.text = "more than 40 times";
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
