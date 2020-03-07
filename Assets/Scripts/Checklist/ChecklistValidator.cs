using System;

public abstract class ChecklistValidator
{
    public Action<string> onError;
    public Action onFail;
    public Action onSuccess;
    public Action<TaskType, bool> onPass;
    protected Checklist checklist;

    public ChecklistValidator(Checklist checklist)
    {
        this.checklist = checklist;
    }

    protected void Error(string err)
    {
        onError?.Invoke(err);
    }

    protected void Fail()
    {
        onFail?.Invoke();
    }

    protected void Success()
    {
        onSuccess?.Invoke();
    }

    protected void Pass(TaskType taskType, bool completed)
    {
        onPass?.Invoke(taskType, completed);
    }

    public abstract void Validate(TaskType taskType, bool completed);
}
