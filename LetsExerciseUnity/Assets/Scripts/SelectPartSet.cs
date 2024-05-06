using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPartSet : MonoBehaviour
{
    // Start is called before the first frame update

    string recommandList;

    List<string> parts = new List<string> { "Abs", "Arms", "Buttocks", "Legs", "Whole Body" };

    void Start()
    {
        recommandList = "";

    }

    public string SetRecommandList( string prefer)
    {
        recommandList = prefer + ",";

        parts.Remove(prefer);

        foreach (string s in parts)
        {
            recommandList += s + ",";
        }

        return recommandList;

    }
}
