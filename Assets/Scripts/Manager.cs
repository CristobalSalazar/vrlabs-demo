using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum SimulationMode { Automatic, Manual }

public class Manager : MonoBehaviour
{
    [SerializeField] private string serverURL;
    [SerializeField] private Animator screenAnimator;
    public Text resultMessage;

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        SimulationEvents.On(EventType.SimulationSuccess, SimulationSuccess);
        SimulationEvents.On(EventType.SimulationFailed, SimulationFailed);
    }

    void OnDestroy()
    {
        SimulationEvents.Unsubscribe(EventType.SimulationSuccess, SimulationSuccess);
        SimulationEvents.Unsubscribe(EventType.SimulationFailed, SimulationFailed);
    }

    private void SimulationSuccess()
    {
        SimulationAttempt.GetInstance().simulationComplete = true;
        DisplaySuccessMessage(SimulationSession.Current.operatorName, SimulationAttempt.GetInstance().GetTime());
        SendSimulationDataToServer();
    }

    private void SimulationFailed()
    {
        SimulationAttempt.GetInstance().simulationComplete = false;
        DisplayFailureMessage();
        SendSimulationDataToServer();
    }

    private void SendSimulationDataToServer()
    {
        SimulationAttempt attempt = SimulationAttempt.GetInstance();
        SimulationSession session = SimulationSession.Current;
        session.AddAttemptData(attempt);
        RequestPostBody requestData = RequestPostBody.CreateFromSessionAndAttempt(session, attempt);
        HttpService.SendPostRequest(requestData, serverURL);
    }

    private void DisplaySuccessMessage(string operatorName, float time)
    {
        if (operatorName == null || operatorName == "") operatorName = "Operador";
        string message = $"{operatorName} completo la simulacion en {time} segundos";
        resultMessage.text = message;
        screenAnimator.Play("Success");
    }

    private void DisplayFailureMessage()
    {
        resultMessage.text = SimulationAttempt.GetErrors().Last();
        screenAnimator.Play("Failure");
    }
}
