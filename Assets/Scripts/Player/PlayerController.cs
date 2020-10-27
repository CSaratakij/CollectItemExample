using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform camera;

    [SerializeField]
    float moveSpeed = 10.0f;

    Vector2 inputVector;
    Vector3 velocity;

    CharacterController characterController;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        InputHandler();
        MovementHandler();
    }

    void Initialize()
    {
        characterController = GetComponent<CharacterController>();
        camera.parent = null;
    }

    void InputHandler()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        inputVector = Vector2.ClampMagnitude(inputVector, 1.0f);
    }

    void MovementHandler()
    {
        var direction = GetCameraFacing(camera);
        velocity = direction * moveSpeed;
        characterController.Move(velocity * Time.deltaTime);
    }

    Vector3 GetCameraFacing(Transform camera)
    {
        var forwardDirection = camera.forward;
        var rightDirection = camera.right;

        forwardDirection.y = 0.0f;
        rightDirection.y = 0.0f;

        forwardDirection = forwardDirection.normalized;
        rightDirection = rightDirection.normalized;

        return (rightDirection * inputVector.x) + (forwardDirection * inputVector.y);
    }
}

