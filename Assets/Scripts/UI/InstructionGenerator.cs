using UnityEngine;

public class InstructionGenerator : MonoBehaviour
{
    [SerializeField] private string _pathToPrefab = "UI/ObjectiveUI";
    private GameObject _objectivePrefab;

    void Start()
    {
        if (LoadPrefab())
        {
            Checklist list = SimulationAttempt.Checklist;
            GenerateInstructions(list);
        }
        else
        {
            Debug.LogError($"Invalid path to prefab cannot find a GameObject at Resources/{_pathToPrefab}");
        }
    }

    void GenerateInstructions(Checklist list)
    {
        Task[] tasks = list.GetAllTasks();

        foreach (var task in tasks)
        {
            CreateObjective(task);
        }
    }

    bool LoadPrefab()
    {
        _objectivePrefab = Resources.Load(_pathToPrefab) as GameObject;
        if (_objectivePrefab == null)
        {
            return false;
        }
        return true;
    }

    void CreateObjective(Task task)
    {
        Checklist checklist = SimulationAttempt.Checklist;
        GameObject obj = Instantiate(_objectivePrefab, transform);
        ObjectiveUI objectiveUI = obj.GetComponent<ObjectiveUI>();
        if (objectiveUI != null)
        {
            objectiveUI.SetText(task.description);
            objectiveUI.SetToggle(task.completed);
            objectiveUI.SubscribeToTaskChange(checklist, task);
        }
    }
}
