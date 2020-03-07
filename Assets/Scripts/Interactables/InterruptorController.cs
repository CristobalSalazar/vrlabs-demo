using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InterruptorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMode(string mode)
    {
        switch (mode)
        {
            case "Auto":
                {
                    SetAutomaticMode();
                    break;
                }
            case "Off":
                {
                    SetOffMode();
                    break;
                }
            case "Man":
                {
                    SetManualMode();
                    break;
                }
        }
    }

    private void SetAutomaticMode()
    {
        SimulationAttempt.SetTask(TaskType.InterruptorAutomatic, true);
        SimulationAttempt.SetTask(TaskType.InterruptorManual, false);
        animator.Play("Auto");
    }

    private void SetManualMode()
    {
        SimulationAttempt.SetTask(TaskType.InterruptorManual, true);
        SimulationAttempt.SetTask(TaskType.InterruptorAutomatic, false);
        animator.Play("Man");
    }

    private void SetOffMode()
    {
        SimulationAttempt.SetTask(TaskType.InterruptorManual, false);
        SimulationAttempt.SetTask(TaskType.InterruptorAutomatic, false);
    }

}