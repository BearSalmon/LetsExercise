using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SelectPartSet : MonoBehaviour
{
    // Start is called before the first frame update
    DBUtils dBUtils;
    User user;
    string recommendation;

    // "Arms", "Abs",  "Buttocks", "Legs", "Whole Body" , "Easy" , "Medium" , "Hard"

    void Start()
    {
        recommendation = "";
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);
    }

    public string SetRecommandation( string prefer)
    {
        recommendation = "";
        string[] recommendValues;
        recommendValues = user.Recommendation.TrimEnd(',').Split(',');

        recommendValues[0] = "0";
        recommendValues[1] = "0";
        recommendValues[2] = "0";
        recommendValues[3] = "0";
        recommendValues[4] = "0";

        if (prefer == "Arms")
        {
            recommendValues[0] = "1";
        }
        else if (prefer == "Abs")
        {
            recommendValues[1] = "1";
        }
        else if (prefer == "Buttocks")
        {
            recommendValues[2] = "1";
        }
        else if (prefer == "Legs")
        {
            recommendValues[3] = "1";
        }
        else if (prefer == "Whole Body")
        {
            recommendValues[4] = "1";
        }

        foreach (string part in recommendValues)
        {
            recommendation += part + ",";
        }
        return recommendation;

    }
}
