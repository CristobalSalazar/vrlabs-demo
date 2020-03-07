using UnityEngine;

public class SafetyMatController : MonoBehaviour
{
    private void Start()
    {
        SimulationAttempt.SetTask(TaskType.NoSafetyMat, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        SimulationAttempt.SetTask(TaskType.NoSafetyMat, false);
    }

    private void OnTriggerExit(Collider other)
    {
        SimulationAttempt.SetTask(TaskType.NoSafetyMat, true);
    }
}
