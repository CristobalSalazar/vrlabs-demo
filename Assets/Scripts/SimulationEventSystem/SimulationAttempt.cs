using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulationAttempt : MonoBehaviour
{
    public static Checklist Checklist { get { return current.checklist; } }
    private ChecklistValidator validator;
    public Checklist checklist { get; private set; }
    private static SimulationAttempt current;
    private Timer timer;
    public bool simulationComplete;
    public List<string> errors { get; private set; } = new List<string>();

    void Awake()
    {
        if (current == null)
        {
            current = this;
            InitChecklistAndValidator();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitTimer();
    }

    void OnDestroy()
    {
        UnsubscribeValidator();
        UnsubscribeTimer();
    }


    public static void SetTask(TaskType taskType, bool completed)
    {
        current.validator.Validate(taskType, completed);
    }

    public static SimulationAttempt GetInstance()
    {
        return current;
    }

    public float GetTime()
    {
        return timer.time;
    }

    public static string[] GetErrors()
    {
        return current.errors.ToArray();
    }

    void InitTimer()
    {
        float failTime = SimulationSession.Current.FAIL_TIME;

        timer = gameObject.AddComponent<Timer>();
        timer.ResetTimer();

        timer.On(failTime, () =>
        {
            // TODO move to checklist SetTask() and add validation
            errors.Add($"No completo la simulacion en el tiempo adecuado de {failTime.ToString()} segundos");
            SimulationEvents.Emit(EventType.SimulationFailed);
        });
        SimulationEvents.On(EventType.SimulationStarted, timer.StartTimer);
        SimulationEvents.On(EventType.SimulationFailed, timer.StopTimer);
        SimulationEvents.On(EventType.SimulationSuccess, timer.StopTimer);
    }

    void InitChecklistAndValidator()
    {
        checklist = ChecklistFactory.Get(SimulationSession.Current.simulationMode);
        validator = ChecklistFactory.GetValidator(SimulationSession.Current.simulationMode, checklist);

        validator.onError += (err) => errors.Add(err);
        validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
        validator.onFail += () =>
        {
            SimulationEvents.Emit(EventType.SimulationFailed);
            simulationComplete = false;
        };
        validator.onSuccess += () =>
        {
            SimulationEvents.Emit(EventType.SimulationSuccess);
            simulationComplete = true;
        };
    }

    void UnsubscribeValidator()
    {
        if (validator == null) return;
        validator.onError -= (err) => errors.Add(err);
        validator.onFail -= () =>
        {
            simulationComplete = false;
            SimulationEvents.Emit(EventType.SimulationFailed);
        };

        validator.onSuccess -= () =>
        {
            simulationComplete = true;
            SimulationEvents.Emit(EventType.SimulationSuccess);
        };
    }

    void UnsubscribeTimer()
    {
        SimulationEvents.Unsubscribe(EventType.SimulationStarted, timer.StartTimer);
        SimulationEvents.Unsubscribe(EventType.SimulationFailed, timer.StopTimer);
        SimulationEvents.Unsubscribe(EventType.SimulationSuccess, timer.StopTimer);
        timer.ClearEvents();
    }
}