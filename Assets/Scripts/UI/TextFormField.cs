using UnityEngine;
using UnityEngine.UI;

public class TextFormField : MonoBehaviour
{
    [SerializeField] private InputField input;
    [SerializeField] private Text label;
    [SerializeField] private Text errorLabel;
    [SerializeField] private string defaultValue = "";
    public InputField.OnChangeEvent onChanged => input.onValueChanged;
    public string value { get; private set; } = "";
    public string errorMessage => errorLabel.text;

    void Start()
    {
        input.onValueChanged.AddListener(HandleInputValueChanged);
        input.text = defaultValue;
        value = defaultValue;
    }

    public void SetErrorMessage(string message)
    {
        errorLabel.text = message;
    }

    public void ClearErrorMessage()
    {
        errorLabel.text = "";
    }

    private void HandleInputValueChanged(string value)
    {
        this.value = value;
    }
}