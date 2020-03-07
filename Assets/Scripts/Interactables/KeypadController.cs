using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour
{
    [SerializeField] private RobotAnimation robotAnimation;
    [SerializeField] private ControllerButton _cycleHold;
    [SerializeField] private ControllerButton _cycleStart;
    [SerializeField] private ControllerButton _reset;
    [SerializeField] private ControllerButton _emergency;

    void Start()
    {
        _cycleHold.onButtonPress += OnCycleHold;
        _cycleStart.onButtonPress += OnCycleStart;
        _reset.onButtonPress += OnReset;
        _emergency.onButtonPress += OnEmergency;
    }

    private void OnCycleStart()
    {
        SimulationAttempt.SetTask(TaskType.CycleStartPressed, true);
    }

    private void OnCycleHold()
    {
        SimulationAttempt.SetTask(TaskType.CycleHoldPressed, true);
    }

    private void OnReset()
    {
        SimulationAttempt.SetTask(TaskType.ResetPressed, true);
    }

    private void OnEmergency()
    {
        SimulationAttempt.SetTask(TaskType.EmergencyPressed, true);
        robotAnimation.StopOperations();
    }
}