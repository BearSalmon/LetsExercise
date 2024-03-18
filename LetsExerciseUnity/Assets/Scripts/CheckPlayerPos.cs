using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPlayerPos : MonoBehaviour
{
    public TextMeshProUGUI pos_message;
    public UDPReceive udpReceive;

    // Start is called before the first frame update
    void Start()
    {
        pos_message = GameObject.Find("PositionMessage").GetComponent<TextMeshProUGUI>();
        udpReceive = GameObject.Find("Manager").GetComponent<UDPReceive>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
