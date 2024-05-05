using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class PythonRunner : MonoBehaviour
{
    Process pythonScript = new Process();
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            // FileName 是要執行的檔案
            string path = Application.streamingAssetsPath + "/LetsExercisePython/main.py";
            string dir = Application.streamingAssetsPath + "/LetsExercisePython";
            string exactPath = Path.GetFullPath("./Assets/LetsExercisePython/main.py");
            pythonScript.StartInfo.FileName = path;
            pythonScript.StartInfo.WorkingDirectory = dir;
            pythonScript.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            pythonScript.Start();
        }
        catch (System.Exception ex)
        {
            string exactPath = Path.GetFullPath("./LetsExercisePython/main.py");
            UnityEngine.Debug.Log("Error running Python script: " + ex.Message);
            UnityEngine.Debug.Log(System.AppDomain.CurrentDomain.BaseDirectory);
            UnityEngine.Debug.Log(exactPath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        pythonScript.Kill();
    }
}
