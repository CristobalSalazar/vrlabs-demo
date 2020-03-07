using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private TextFormField timeInput;
    [SerializeField] private Slider simulationSlider;

    private string _operatorName;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
        _button.onClick.AddListener(OnButtonPress);
        nameInput.onValueChanged.AddListener(HandleNameInputChanged);
        timeInput.onChanged.AddListener(HandleTimeInputChanged);
    }

    private void HandleNameInputChanged(string val)
    {
        _operatorName = val;
        CheckIsValid();
    }

    private void HandleTimeInputChanged(string val)
    {
        if (val.Length == 0 || val == "")
        {
            timeInput.ClearErrorMessage();
            return;
        }
        try
        {
            float time = float.Parse(val);
            timeInput.ClearErrorMessage();
        }
        catch
        {
            timeInput.SetErrorMessage("Por favor ingrese un número valido");
        }
        CheckIsValid();
    }

    private void OnButtonPress()
    {
        SimulationSession.Current.ResetSession();
        SimulationSession.Current.SetOperator(_operatorName);
        SimulationSession.Current.SetFailTime(float.Parse(timeInput.value));

        float mode = simulationSlider.value;
        if (mode == 0)
        {
            SimulationSession.Current.SetMode(SimulationMode.Automatic);
        }
        else if (mode == 1)
        {
            SimulationSession.Current.SetMode(SimulationMode.Manual);
        }

        SceneManager.LoadScene("Manual");
    }

    private void SetInteractableState(bool state)
    {
        _button.interactable = state;
    }

    private void CheckIsValid()
    {
        string errorText = timeInput.errorMessage;
        int nameLength = nameInput.text.Length;
        int timeLength = timeInput.value.Length;

        bool isValid = errorText == "" && timeLength >= 1 && nameLength >= 1;
        SetInteractableState(isValid);
    }
}