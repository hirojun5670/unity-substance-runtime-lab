using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputActionSwitcher : MonoBehaviour
{
    [SerializeField] InputSystemUIInputModule inputSystemUIInputModule;

    [SerializeField] bool isOverUI = false;

    private InputActionMap playerActionMap;

    void Awake()
    {
        playerActionMap = inputSystemUIInputModule.actionsAsset.FindActionMap("Player");
    }

    // Update is called once per frame
    void Update()
    {
        isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

        if (isOverUI && playerActionMap.enabled)
        {
            playerActionMap.Disable();
        }
        else if (!isOverUI && !playerActionMap.enabled)
        {
            playerActionMap.Enable();
        }
    }
}
