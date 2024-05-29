using UnityEngine;

public class GummyBearMove : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public float maxRotationAngle = 20f;

    private float currentAngle;
    private bool rotatingForward = true;

    void Start()
    {
        // Initialize currentAngle with the current Z rotation angle of the GameObject
        currentAngle = transform.localRotation.eulerAngles.z;

        // Normalize currentAngle to be within the range -180 to 180
        if (currentAngle > 180f)
        {
            currentAngle -= 360f;
        }
    }

    void Update()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;
        if (rotatingForward)
        {
            currentAngle += rotationThisFrame;
            if (currentAngle >= maxRotationAngle)
            {
                currentAngle = maxRotationAngle;
                rotatingForward = false;
            }
        }
        else
        {
            currentAngle -= rotationThisFrame;
            if (currentAngle <= -maxRotationAngle)
            {
                currentAngle = -maxRotationAngle;
                rotatingForward = true;
            }
        }

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
