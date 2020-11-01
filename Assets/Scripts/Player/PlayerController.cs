using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField]
    float moveSpeed = 10.0f;

    [SerializeField]
    float jumpSpeed = 20.0f;

    [SerializeField]
    float terminalVelocity = 1000.0f;

    [SerializeField]
    float gravity = 9.8f;

    [SerializeField]
    float gravityScale = 1.0f;

    [Header("Dependencies")]
    [SerializeField]
    Transform camera;

    bool isPressJump;

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
        isPressJump = Input.GetButton("Jump");
    }

    void MovementHandler()
    {
        if (characterController.isGrounded) {
            var direction = GetCameraFacing(camera);
            velocity = (direction * moveSpeed);
            velocity.y = (isPressJump) ? jumpSpeed : 0.0f;
        }

        velocity.y -= (gravity * gravityScale) * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, -terminalVelocity, terminalVelocity);

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

