using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAbility : MonoBehaviour
{
    Action<bool, GameObject> OnFoundSomething;

    [SerializeField]
    float maxInteractableDistance = 5.0f;

    [SerializeField]
    float maxRayDistance = 100.0f;

    [SerializeField]
    LayerMask interactableMask;

    public bool IsInteractable { get; private set; }
    public GameObject CurrentFoundObject => currentFoundObject;

    bool currentFoundState;
    bool previousFoundState;

    float currentDistance = 0.0f;

    GameObject currentFoundObject;
    Camera mainCamera;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        PickupHandler();
    }

    void FixedUpdate()
    {
        CheckInteractableObject();
    }

    void PickupHandler()
    {
        IsInteractable = currentFoundState &&
                        (currentFoundObject != null) &&
                        (currentDistance <= maxInteractableDistance);

        bool isAbleToPickUp = (Input.GetKeyDown(KeyCode.E) && IsInteractable);

        if (isAbleToPickUp) {
            // TODO : What you want to do when player pick this object up is here...
            // Maybe, check the type of object first....
            // For an example, a door is interactable but cannot pick able.
            currentFoundObject.SetActive(false);
        }
    }

    void CheckInteractableObject()
    {
        var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        previousFoundState = currentFoundState;
        currentFoundState = Physics.Raycast(ray, out hit, maxRayDistance, interactableMask);

        currentDistance = hit.distance;

        currentFoundObject = (currentFoundState) ? hit.transform.gameObject : null;
        bool isFoundStateChange = (currentFoundState != previousFoundState);

        if (isFoundStateChange) {
            OnFoundSomething?.Invoke(currentFoundState, currentFoundObject);
        }
    }
}

