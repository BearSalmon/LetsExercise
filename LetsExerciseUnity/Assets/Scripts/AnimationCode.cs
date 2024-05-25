using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;

public class AnimationCode : MonoBehaviour
{
    public GameObject[] Body_for_Exercise;
    public GameObject[] Body_for_Ready;
    List<string> lines;
    public int counter = 0;
    public int loop_cnt = 0;
    public UDPSend udpsend;
    public UDPReceive udpreceive;

    public WholeSampleSceneManager wholeSampleSceneManager;

    public CountDownTimer countDownTimer;

    public bool isAnimating;


    

    // Start is called before the first frame update
    void Start()
    {
        countDownTimer = GetComponent<CountDownTimer>();
        isAnimating = false;
        wholeSampleSceneManager = GetComponent<WholeSampleSceneManager>();
  
        udpsend = GameObject.Find("WholeManager").GetComponent<UDPSend>();
        udpreceive = GameObject.Find("WholeManager").GetComponent<UDPReceive>();

        Body_for_Ready = new GameObject[33];
        Body_for_Exercise = new GameObject[33];
        for (var i = 0; i <= 32; i++)
        {
            GameObject gb = GameObject.Find("r" + i);
            Body_for_Ready[i] = gb;
        }

    }

    public void setBodyList()
    {
        
        if (wholeSampleSceneManager.nowState == 0)
        {
            for (var i = 0; i <= 32; i++)
            {
                GameObject gb = GameObject.Find("e" + i);
                Body_for_Exercise[i] = gb;
            }
        }
        else
        {
            for (var i = 0; i <= 32; i++)
            {
                GameObject gb = GameObject.Find("r" + i);
                Body_for_Ready[i] = gb;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        udpreceive.canContinue = true;
        if (isAnimating && udpreceive.canContinue)
        {
            udpsend.SendDataForCounter(counter.ToString());
            string[] points = lines[counter++].Split(',');

            for (int i = 0; i <= 32; i++)
            {
                // normoalize position
                float x = float.Parse(points[0 + (i * 3)]) / 20;
                float y = float.Parse(points[1 + (i * 3)]) / 20;
                float z = float.Parse(points[2 + (i * 3)]) / 500;


                if (wholeSampleSceneManager.nowState == 0)
                {
                    Body_for_Exercise[i].transform.localPosition = new Vector3(x, y, z);
                }
                else
                {
                    Body_for_Ready[i].transform.localPosition = new Vector3(x, y, z);
                }
                
            }
            if (counter >= lines.Count)
            {
                counter = 0;
                loop_cnt += 1;
            }

            while (!udpreceive.canReadNextLine) ;
            udpreceive.canReadNextLine = false;
        }
        
    }

    public void StartAnimation()
    {
        isAnimating = true;
    }

    public void StopAnimation()
    {
        isAnimating = false;
    }

    public void ChangeLineList(string path)
    {
        lines = System.IO.File.ReadLines(Application.streamingAssetsPath + "/LetsExercisePython" + path).ToList();
        counter = 0;
    }
}
