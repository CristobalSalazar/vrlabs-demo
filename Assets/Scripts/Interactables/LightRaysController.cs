using UnityEngine;

public class LightRaysController : MonoBehaviour
{
    private bool isDeactivated;

    void Start()
    {
        SimulationAttempt.SetTask(TaskType.NoLightRays, true);
        SimulationAttempt.Checklist.OnTaskChanged.AddListener((TaskType taskType, Task t) =>
        {
            if (taskType == TaskType.RobotT1)
            {
                Deactivate();
            }
        });

        SimulationAttempt.Checklist.OnTaskChanged.AddListener((TaskType taskType, Task t) =>
        {
            if (t.completed && taskType == TaskType.InterruptorAutomatic)
            {
                Activate();
            }
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDeactivated) return;
        SimulationAttempt.SetTask(TaskType.NoLightRays, false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDeactivated) return;
        SimulationAttempt.SetTask(TaskType.NoLightRays, true);
    }

    public void Deactivate()
    {
        isDeactivated = true;
        gameObject.SetActive(false);
        MessageUI.DisplayMessage("Cortinas de luz desactivadas");
    }

    public void Activate()
    {
        isDeactivated = false;
        gameObject.SetActive(true);
        if (SimulationSession.Current.simulationMode == SimulationMode.Manual)
        {
            MessageUI.DisplayMessage("Cortinas de luz reactivadas");
        }
    }
}
