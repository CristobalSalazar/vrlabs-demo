using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DoorController : MonoBehaviour, Interactable
{
    [SerializeField] private Animator animator;
    public static DoorController main;
    public string description { get; set; }
    public static bool isOpen { get; private set; }
    public bool canInteract { get; set; }

    void Awake()
    {
        // singleton
        if (main == null) main = this;
        else Destroy(this);
    }

    void Start()
    {
        isOpen = false;
        canInteract = true;
        animator.enabled = false;
        Interact();
    }

    public void Interact()
    {
        if (isOpen)
        {
            CloseDoor();
            SimulationAttempt.SetTask(TaskType.DoorClosed, true);
        }
        else
        {
            OpenDoor();
            SimulationAttempt.SetTask(TaskType.DoorClosed, false);
        }
        isOpen = !isOpen;
    }

    private void CloseDoor()
    {
        animator.SetBool("isOpen", false);
        description = "Abrir";
    }

    private void OpenDoor()
    {
        if (!animator.isActiveAndEnabled)
        {
            animator.enabled = true;
        }
        else
        {
            animator.SetBool("isOpen", true);
        }
        description = "Cerrar";
    }

}