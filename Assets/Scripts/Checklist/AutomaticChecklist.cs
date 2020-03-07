using System.Collections.Generic;

public sealed class AutomaticChecklist : Checklist
{
    public AutomaticChecklist()
    {
        tasks = new Dictionary<TaskType, Task>
        { { TaskType.NoSafetyMat, new Task(TaskType.NoSafetyMat, "Falta de presencia de operador en tapete de seguridad") },
            { TaskType.NoLightRays, new Task(TaskType.NoLightRays, "Falta de presencia de operador en las cortinas de luz") },
            { TaskType.PiecePlaced, new Task(TaskType.PiecePlaced, "Pieza colocada") },
            { TaskType.DoorClosed, new Task(TaskType.DoorClosed, "Puerta de celda cerrada") },
            { TaskType.InterruptorAutomatic, new Task(TaskType.InterruptorAutomatic, "Interruptor en modo automatico") },
            { TaskType.ResetPressed, new Task(TaskType.ResetPressed, "Pressionar el boton Reset") },
            { TaskType.CycleStartPressed, new Task(TaskType.CycleStartPressed, "Pressionar el boton Cycle Start") },
        };
    }
}