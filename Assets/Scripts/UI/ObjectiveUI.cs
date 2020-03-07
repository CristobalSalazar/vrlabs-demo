using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Text _text;

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetToggle(bool value)
    {
        _toggle.isOn = value;
    }

    public void SubscribeToTaskChange(Checklist checklist, Task task)
    {
        SimulationAttempt.Checklist.OnTaskChanged.AddListener((TaskType taskType, Task taskChanged) =>
        {
            if (taskType == task.taskType)
            {
                SetToggle(taskChanged.completed);
            }
        });
    }
}
