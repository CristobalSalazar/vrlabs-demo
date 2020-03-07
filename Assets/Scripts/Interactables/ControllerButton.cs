using System;
using UnityEngine;

public class ControllerButton : MonoBehaviour, Interactable
{
    public string buttonName;
    public string description { get; set; }
    public bool canInteract { get; set; }
    public Action onButtonPress;

    public void Interact()
    {
        onButtonPress.Invoke();
    }

    void Start()
    {
        description = buttonName;
        canInteract = true;
    }
}