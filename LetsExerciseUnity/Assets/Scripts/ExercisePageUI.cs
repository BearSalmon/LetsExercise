using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExercisePageUI : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI message;
    public TextMeshProUGUI pos_message;
    public UDPReceive udpReceive;


    void Start()
    {
        //pos_message = GameObject.Find("PositionMessage").GetComponent<TextMeshProUGUI>();
        udpReceive = GameObject.Find("Manager").GetComponent<UDPReceive>();
    }


    // Update is called once per frame
    void Update()
    {
        //message.text = udpReceive.dataAngle;
        //pos_message.text = udpReceive.dataPos;
    }

   
}
