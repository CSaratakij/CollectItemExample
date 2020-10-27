using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAbility : MonoBehaviour
{
    Action<bool> OnFoundSomething;

    [SerializeField]
    float maxRayDistance = 1000.0f;

    [SerializeField]
    LayerMask interactableMask;

    bool currentFoundState;
    bool previousFoundState;

    Camera mainCamera;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        CheckInteractableObject();
    }

    void CheckInteractableObject()
    {
        var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        previousFoundState = currentFoundState;
        currentFoundState = Physics.Raycast(ray, out hit, maxRayDistance, interactableMask);

        bool isFoundStateChange = (currentFoundState != previousFoundState);

        if (isFoundStateChange) {
            OnFoundSomething?.Invoke(currentFoundState);
        }
    }
}

