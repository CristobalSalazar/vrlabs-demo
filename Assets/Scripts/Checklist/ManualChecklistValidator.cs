using UnityEngine;
public sealed class ManualChecklistValidator : ChecklistValidator
{
    public ManualChecklistValidator(Checklist checklist) : base(checklist) { }

    public override void Validate(TaskType taskType, bool completed)
    {
        switch (taskType)
        {
            case TaskType.InterruptorManual:
                {
                    Pass(taskType, true);
                    break;
                }
            case TaskType.RobotT1:
                {
                    RobotT1(taskType, completed);
                    break;
                }
            case TaskType.RobotFixed:
                {
                    RobotFixed(taskType, completed);
                    break;
                }
            case TaskType.NoLightRays:
                {
                    NoLightRays(taskType, completed);
                    break;
                }
            case TaskType.CycleStartPressed:
                {
                    CycleStart(taskType, completed);
                    break;
                }
            case TaskType.CycleHoldPressed:
                {
                    CycleHold(taskType, completed);
                    break;
                }
            case TaskType.ResetPressed:
                {
                    Reset(taskType, completed);
                    break;
                }
            case TaskType.EmergencyPressed:
                {
                    Emergency(taskType, completed);
                    break;
                }
            case TaskType.InterruptorAutomatic:
                {
                    InterruptorAutomatic(taskType, completed);
                    break;
                }
            default:
                {
                    Pass(taskType, completed);
                    break;
                }
        }
    }

    void RobotT1(TaskType taskType, bool completed)
    {
        if (completed)
        {
            bool doorWasOpen = !checklist.GetTask(TaskType.DoorClosed).completed;
            bool wasCycleHoldPressed = checklist.GetTask(TaskType.CycleHoldPressed).completed;
            bool wasInterruptorManual = checklist.GetTask(TaskType.InterruptorManual).completed;
            bool willFail = false;

            if (doorWasOpen)
            {
                Error("Presiono el boton para colocar el robot en posicion t1 con la puerta abierta.");
                willFail = true;
            }
            if (!wasCycleHoldPressed)
            {
                Error("Presiono el boton para colocar el robot en posicion t1 antes de presionar Cycle Hold");
                willFail = true;
            }
            if (!wasInterruptorManual)
            {
                Error("Presiono el boton para colocar el robot en posicion t1 antes sin poner el interruptor en modo manual");
                willFail = true;
            }

            if (willFail)
                Fail();
            else
                Pass(taskType, completed);


        }
        else
            Pass(taskType, completed);
    }

    void CycleStart(TaskType taskType, bool completed)
    {
        if (completed)
        {
            Error("Presiono Cycle Start.");
            Fail();
        }
        else
            Pass(taskType, completed);
    }

    void CycleHold(TaskType taskType, bool completed)
    {
        if (completed)
        {
            bool wasDoorOpen = !checklist.GetTask(TaskType.DoorClosed).completed;
            if (wasDoorOpen)
            {
                Error("Presiono Cycle Hold con la puerta abierta.");
                Fail();
            }
            else
                Pass(taskType, completed);
        }
        else
            Pass(taskType, completed);
    }

    void Reset(TaskType taskType, bool completed)
    {
        if (completed)
        {
            bool wasDoorOpen = !checklist.GetTask(TaskType.DoorClosed).completed;
            bool wasCycleHoldPressed = checklist.GetTask(TaskType.CycleHoldPressed).completed;
            bool wasModeAutomatic = checklist.GetTask(TaskType.InterruptorAutomatic).completed;
            bool willFail = false;

            if (wasDoorOpen)
            {
                Error("La puerta esta abierta al presionar Reset.");
                willFail = true;
            }

            if (!wasModeAutomatic)
            {
                Error("Presiono Reset sin haber puesto el interuptor en modo automatico.");
                willFail = true;
            }

            if (!wasCycleHoldPressed)
            {
                Error("Presiono Reset antes de presionar Cycle Hold.");
                willFail = true;
            }

            if (willFail)
            {
                Debug.Log("FAILING");
                Fail();
            }
            else
            {
                Success();
                Pass(taskType, completed);
            }
        }
        else
            Pass(taskType, completed);
    }

    void Emergency(TaskType taskType, bool completed)
    {
        if (completed)
        {
            Error("Presiono el boton de emergencia.");
            Fail();
        }
        else
            Pass(taskType, completed);
    }

    void NoLightRays(TaskType taskType, bool completed)
    {
        if (!completed)
        {
            Error("Presencia de operador en las cortinas de luz.");
            Fail();
        }
        else
            Pass(taskType, completed);
    }

    void InterruptorAutomatic(TaskType taskType, bool completed)
    {
        if (completed)
        {
            bool wasInterruptorManual = checklist.GetTask(TaskType.InterruptorManual).completed;
            bool wasRobotFixed = checklist.GetTask(TaskType.RobotFixed).completed;
            bool fail = false;

            if (!wasInterruptorManual)
            {
                Error("Puso el interruptor en modo auto antes de ponerlo en modo manual.");
                fail = true;
            }

            if (!wasRobotFixed)
            {
                Error("Puso el interruptor en modo auto sin haber dado toque al robot.");
                fail = true;
            }

            if (fail)
                Fail();
            else
                Pass(taskType, completed);
        }
        else
            Pass(taskType, completed);
    }

    private void RobotFixed(TaskType taskType, bool completed)
    {
        bool wasT1Pressed = checklist.GetTask(TaskType.RobotT1).completed;

        if (!wasT1Pressed)
        {
            Error("Dio toque al robot sin haberlo colocado en posicion t1");
            Fail();
        }
        else
            Pass(taskType, completed);
    }


}
