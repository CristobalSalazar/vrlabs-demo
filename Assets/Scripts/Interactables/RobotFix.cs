using UnityEngine;

public class RobotFix : MonoBehaviour, Interactable
{
    public bool canInteract { get; set; }
    public string description { get; set; }

    void Start()
    {
        canInteract = true;
        description = "Retocar";
    }

    public void Interact()
    {
        SimulationAttempt.SetTask(TaskType.RobotFixed, true);
        canInteract = false;
    }
}