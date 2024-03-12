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
        // FileName 是要執行的檔案
        pythonScript.StartInfo.FileName = "./LetsExercisePython/main.py";
        pythonScript.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
