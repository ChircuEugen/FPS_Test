using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputReader playerInput;

    [SerializeField] private float sensitivity = 3f;

    public Transform target;

    private float rotationX;
    private float rotationY;

    public float distanceFromTarget = 3f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    private float smoothTime = 0.1f;

    private Vector2 rotationXMinMax = new Vector2(-20, 30);

    private void Start()
    {
        playerInput = target.GetComponentInParent<InputReader>();
    }

    private void Update()
    {
        float mouseX = playerInput.lookAction.ReadValue<Vector2>().x * sensitivity;
        float mouseY = playerInput.lookAction.ReadValue<Vector2>().y * sensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;


        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
        Vector3 nextRotation = new Vector3(rotationX, rotationY);


        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distanceFromTarget + transform.right * 1;

    }
}
