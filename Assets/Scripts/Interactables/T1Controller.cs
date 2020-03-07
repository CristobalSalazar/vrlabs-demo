using UnityEngine;

public class T1Controller : MonoBehaviour, Interactable
{
    public bool canInteract { get; set; }
    public string description { get; set; }

    void Start()
    {
        canInteract = true;
        description = "Meter robot en posicion T1";
    }

    public void Interact()
    {
        SimulationAttempt.SetTask(TaskType.RobotT1, true);
        canInteract = false;
    }
}
