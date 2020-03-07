using System.Collections.Generic;

public class ChecklistEvents
{
    public delegate void TaskListener(TaskType taskType, Task task);
    private List<TaskListener> list = new List<TaskListener>();

    public void Emit(TaskType taskType, Task task)
    {
        foreach (var listener in list)
        {
            listener?.Invoke(taskType, task);
        }
    }

    public void AddListener(TaskListener taskListener)
    {
        list.Add(taskListener);
    }

    public void RemoveListener(TaskListener taskListener)
    {
        list.Remove(taskListener);
    }

    public void RemoveAllListeners()
    {
        list.Clear();
    }
}