using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PythonRunner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Process pythonScript = new Process();
        // FileName �O�n���檺�ɮ�
        pythonScript.StartInfo.FileName = "./LetsExercisePython/main.py";
        pythonScript.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
