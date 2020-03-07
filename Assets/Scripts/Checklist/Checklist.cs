using System.Collections.Generic;

public abstract class Checklist
{
    public ChecklistEvents OnTaskChanged { get; private set; } = new ChecklistEvents();
    protected Dictionary<TaskType, Task> tasks = new Dictionary<TaskType, Task>();

    public Task GetTask(TaskType taskType)
    {
        Task task;
        tasks.TryGetValue(taskType, out task);
        if (task == null)
        {
            return new Task(taskType, "Not implemented", false);
        }
        return task;
    }

    public Task[] GetAllTasks()
    {
        Task[] outTasks = new Task[tasks.Count];
        tasks.Values.CopyTo(outTasks, 0);
        return outTasks;
    }

    public List<Task> GetCompletedTasks()
    {
        List<Task> completedTasks = new List<Task>();
        foreach (Task task in tasks.Values)
        {
            if (task.completed)
                completedTasks.Add(task);
        }
        return completedTasks;
    }

    public void SetTaskCompletion(TaskType taskType, bool complete)
    {
        Task task;
        tasks.TryGetValue(taskType, out task);
        if (task == null)
        {
            return;
        }
        else
        {
            task.completed = complete;
            tasks[taskType] = task;
            OnTaskChanged.Emit(taskType, task);
        }
    }

    public bool AllCompleted()
    {
        foreach (Task task in tasks.Values)
        {
            if (!task.completed)
            {
                return false;
            }
        }
        return true;
    }

}