using System.Collections.Generic;

public class ManualChecklist : Checklist
{
    public ManualChecklist()
    {
        tasks = new Dictionary<TaskType, Task>
        { { TaskType.NoSafetyMat, new Task(TaskType.NoSafetyMat, "Falta de presencia de operador en tapete de seguridad") },
            { TaskType.NoLightRays, new Task(TaskType.NoLightRays, "Falta de presencia de operador en las cortinas de luz") },
            { TaskType.DoorClosed, new Task(TaskType.DoorClosed, "Puerta de celda cerrada") },
            { TaskType.CycleHoldPressed, new Task(TaskType.CycleHoldPressed, "Pressionar el boton Cycle Hold") },
            { TaskType.InterruptorManual, new Task(TaskType.InterruptorManual, "Interruptor en modo manual") },
            { TaskType.RobotT1, new Task(TaskType.RobotT1, "Colocar el controlador del Robot en posicion manual llamada T1") },
            { TaskType.RobotFixed, new Task(TaskType.RobotFixed, "Entra en la celda por la parte trasera y da un toque al robot") },
            { TaskType.InterruptorAutomatic, new Task(TaskType.InterruptorAutomatic, "Interruptor en modo automatico") },
            { TaskType.ResetPressed, new Task(TaskType.ResetPressed, "Pressionar el boton Resets para borrar possibles fallos") },
        };
    }
}