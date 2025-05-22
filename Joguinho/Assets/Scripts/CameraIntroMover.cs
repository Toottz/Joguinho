using UnityEngine;

public class CameraIntroMover : MonoBehaviour
{
    public Transform targetPosition;
    public float moveDuration = 2f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float elapsedTime = 0f;
    private bool isMoving = true;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            //transform.position = Vector3.Lerp(initialPosition, targetPosition.position, t);
            //transform.rotation = Quaternion.Slerp(initialRotation, targetPosition.rotation, t);

            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }

    public bool IsMoving()
    {
        return elapsedTime < moveDuration;
    }
}
