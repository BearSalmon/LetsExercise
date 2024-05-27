using System.Collections;
using UnityEngine;

public class PentagonMovement : MonoBehaviour
{
    public GameObject[] circles; // Assign your circle GameObjects in the Inspector
    public float duration = 5f; // Duration to complete one loop

    private Vector2[] positions;

    void Start()
    {
        // Define the pentagon vertices
        positions = new Vector2[]
        {
            new Vector2(0, 220),
            new Vector2(-300, 0),
            new Vector2(-180, -325),
            new Vector2(180, -325),
            new Vector2(300, 0)
        };

        // Start the movement coroutine
        StartCoroutine(MoveCircles(0));
        StartCoroutine(MoveCircles(1));
        StartCoroutine(MoveCircles(2));
        StartCoroutine(MoveCircles(3));
        StartCoroutine(MoveCircles(4));
    }

    private IEnumerator MoveCircles(int n)
    {
        float elapsedTime = 0f;
        float initialSpeed = 1.2f; // Initial speed factor
        float finalSpeed = 0.6f; // Initial speed factor
        float speed = initialSpeed;

        while (speed > finalSpeed)
        {
            for (int i = n, j = 0; j < positions.Length; i = (i + 1) % 5, j++)
            {
                Vector2 startPosition = positions[i];
                Vector2 endPosition = positions[(i + 1) % positions.Length];
                //float elapsedTime = 0f;

                while (elapsedTime < duration / positions.Length)
                {
                    //foreach (GameObject circle in circles)
                    //{
                    //    circle.transform.localPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / (duration / positions.Length));
                    //}
                    circles[n].transform.localPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / (duration / positions.Length));
                    elapsedTime += Time.deltaTime * speed;
                    yield return null;
                }
                elapsedTime = 0f;
                speed = Mathf.Lerp(initialSpeed, finalSpeed, (float)j / (positions.Length - 1));
            }
        }
    }
}

