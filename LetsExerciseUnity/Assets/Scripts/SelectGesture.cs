using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGesture : MonoBehaviour
{

    // for test 


    public Button button;
    public UDPSend udpSend;


    public CountDownTimer countDownTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClick());
        udpSend = GameObject.Find("Manager").GetComponent<UDPSend>();
        countDownTimer = GetComponent<CountDownTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClick()
    {
        udpSend.SendData("start");
        countDownTimer.StartCountDown(5f);

    }


}
