using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 10.0f;

    [SerializeField]
    float sensitivityMultiplier = 1.0f;

    [SerializeField]
    Vector3 maxAxisRotation = new Vector3(85.0f, 360.0f, 45.0f);

    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 targetOffset;

    Vector3 rotateAxis;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        InputHandler();
        RotateHandler();
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void InputHandler()
    {
        var multipiler = (sensitivity * sensitivityMultiplier);

        rotateAxis.x += -Input.GetAxisRaw("Mouse Y") * multipiler;
        rotateAxis.y += Input.GetAxisRaw("Mouse X") * multipiler;

        rotateAxis.x = Mathf.Clamp(rotateAxis.x, -maxAxisRotation.x, maxAxisRotation.x);

        bool isReachTheLimitInYAxis = (rotateAxis.y > maxAxisRotation.y) || (rotateAxis.y < -maxAxisRotation.y);

        if (isReachTheLimitInYAxis) {
            rotateAxis.y -= maxAxisRotation.y;
        }
    }

    void RotateHandler()
    {
        var resultRotation = Quaternion.Euler(rotateAxis);
        transform.rotation = resultRotation;
    }

    void FollowTarget()
    {
        transform.position = (target.position + targetOffset);
    }
}

