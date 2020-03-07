using UnityEngine;

public class SimulationSession : MonoBehaviour
{
    public static SimulationSession Current { get; private set; }
    public string operatorName;
    public SimulationMode simulationMode = SimulationMode.Automatic;
    public int totalAttempts;
    public float totalSeconds;
    public int totalErrors;
    public float FAIL_TIME { get; private set; } = 120;


    void Awake()
    {
        if (Current == null)
        {
            Current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetFailTime(float time)
    {
        FAIL_TIME = time;
    }

    public void SetOperator(string name)
    {
        operatorName = name;
    }

    public void SetMode(SimulationMode mode)
    {
        simulationMode = mode;
    }

    public void AddAttemptData(SimulationAttempt attempt)
    {
        totalErrors += attempt.errors.Count;
        totalAttempts++;
        totalSeconds += attempt.GetTime();
    }

    public void ResetSession()
    {
        operatorName = "";
        totalAttempts = 0;
        totalSeconds = 0;
        totalErrors = 0;
        simulationMode = SimulationMode.Automatic;
    }
}