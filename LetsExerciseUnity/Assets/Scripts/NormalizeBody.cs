using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeBody : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject r24;
    public float scaleValue1;
    public float scaleValue2;
    public float shiftHeight;
    public List<GameObject> bodies;
    void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //if (lineRenderer.positionCount >= 2)
        //{
        //    // Get the positions of the line's points
        //    Vector3 startPoint = lineRenderer.GetPosition(0);
        //    Vector3 endPoint = lineRenderer.GetPosition(1);

        //    // Calculate the distance between the points
        //    float lineLength = Vector3.Distance(startPoint, endPoint);
        //    scaleValue = 4 * (lineLength / 20);

        //    // Output the length to the console
        //    //Debug.Log("Length of the line: " + lineLength);
        //}
    }
    public void changeBodyScale()
    {
        if (lineRenderer.positionCount >= 2)
        {
            // Get the positions of the line's points
            Vector3 startPoint = lineRenderer.GetPosition(0);
            Vector3 endPoint = lineRenderer.GetPosition(1);

            // Calculate the distance between the points
            float lineLength = Vector3.Distance(startPoint, endPoint);
            scaleValue1 = 4 * (22 / lineLength);
            scaleValue2 = 9 * (22 / lineLength);
            shiftHeight = 220 - r24.transform.position.y;

            // Output the length to the console
            Debug.Log("Length of the line: " + lineLength);
        }
        int idx = 0;
        foreach (GameObject body in bodies)
        {
            body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y + shiftHeight, body.transform.position.z);
            if (idx == 0) // exercise
            {
                if (body.activeSelf == false)
                {
                    gameObject.SetActive(true);
                    body.transform.localScale = new Vector3(scaleValue1, scaleValue1, scaleValue1);
                    gameObject.SetActive(false);
                }
                else
                {
                    body.transform.localScale = new Vector3(scaleValue1, scaleValue1, scaleValue1);
                }
            }
            else // ready
            {
                if (body.activeSelf == false)
                {
                    gameObject.SetActive(true);
                    body.transform.localScale = new Vector3(scaleValue2, scaleValue2, scaleValue2);
                    gameObject.SetActive(false);
                }
                else
                {
                    body.transform.localScale = new Vector3(scaleValue2, scaleValue2, scaleValue2);
                }
            }         
            idx++;
        }
        //animationCode.positionSet = false;
        Debug.Log(r24.transform.position.y);
    }
}
