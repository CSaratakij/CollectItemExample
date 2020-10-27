using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableStatus : MonoBehaviour
{
    const int UPDATE_RATE = 2;
    const string LABEL_FORMAT = "[ E ] {0}";

    [SerializeField]
    Text lblInteractStatus;

    [SerializeField]
    InteractAbility interactAbility;

    [SerializeField]
    LayerMask pickupLayer;

    [SerializeField]
    LayerMask interactLayer;

    void Awake()
    {
        Initialize();
    }

    void LateUpdate()
    {
        if (Time.frameCount % UPDATE_RATE == 0) {
            UpdateUI();
        }
    }

    void Initialize()
    {
        lblInteractStatus.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        var obj = interactAbility.CurrentFoundObject;

        if (obj == null) {
            lblInteractStatus.gameObject.SetActive(false);
            return;
        }

        if (IsInLayerMask(obj, pickupLayer)) {
            lblInteractStatus.text = string.Format(LABEL_FORMAT, "Pick Up");
        }
        else if (IsInLayerMask(obj, interactLayer)) {
            lblInteractStatus.text = string.Format(LABEL_FORMAT, "Interact");
        }

        lblInteractStatus.gameObject.SetActive(interactAbility.IsInteractable);
    }

    bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        int objLayerMask = (1 << obj.layer);

        if ((layerMask.value & objLayerMask) > 0) {
            return true;
        }
        else {
            return false;
        }
    }
}

