using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class AnimationCode : MonoBehaviour
{
    public GameObject[] Body;
    List<string> lines;
    public int counter = 0;
    public int loop_cnt = 0;
    public UDPSend udpsend;
    public UDPReceive udpreceive;


    public CountDownTimer countDownTimer;

    public bool isAnimating;
    

    // Start is called before the first frame update
    void Start()
    {
        lines = System.IO.File.ReadLines("Assets/PoseDataset/arms/arm1.txt").ToList();
        countDownTimer = GameObject.Find("ManagerToBeKeep").GetComponent<CountDownTimer>();
        isAnimating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            udpsend.SendData(counter.ToString());
            string[] points = lines[counter++].Split(',');

            for (int i = 0; i <= 32; i++)
            {
                // normoalize position
                float x = float.Parse(points[0 + (i * 3)]) / 20;
                float y = float.Parse(points[1 + (i * 3)]) / 20;
                float z = float.Parse(points[2 + (i * 3)]) / 500;

                Body[i].transform.localPosition = new Vector3(x, y, z);
            }
            if (counter >= lines.Count)
            {
                counter = 0;
                loop_cnt += 1;
            }

            while (!udpreceive.canContinue) ;
            udpreceive.canContinue = false;

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
}
