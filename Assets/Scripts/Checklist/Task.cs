public class Task
{
    public string description;
    public bool completed;
    public TaskType taskType;

    public Task(TaskType taskType, string description, bool completed = false)
    {
        this.taskType = taskType;
        this.description = description;
        this.completed = completed;
    }
}