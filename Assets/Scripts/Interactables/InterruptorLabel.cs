using UnityEngine;

public class InterruptorLabel : MonoBehaviour, Interactable
{
    public bool canInteract { get; set; }
    public string description { get; set; }

    [SerializeField] private string _description;
    [SerializeField] private InterruptorController interruptor;

    void Start()
    {
        canInteract = true;
        description = _description;
    }

    public void Interact()
    {
        interruptor.SetMode(description);
    }
}