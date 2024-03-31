using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExercisePageUI : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI wrong_message;
    public TextMeshProUGUI pos_message;
    public UDPReceive udpReceive;
    public GameObject exercise;


    void Start()
    {
        udpReceive = GameObject.Find("Manager").GetComponent<UDPReceive>();
    }


    // Update is called once per frame
    void Update()
    {
        if (exercise.activeSelf == true)
        {
            wrong_message.text = udpReceive.dataAngle;
            pos_message.text = udpReceive.dataPos;
        }
    }  
}
