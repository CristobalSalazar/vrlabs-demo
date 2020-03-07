public sealed class AutomaticChecklistValidator : ChecklistValidator
{
    public AutomaticChecklistValidator(Checklist checklist) : base(checklist)
    {
    }

    public override void Validate(TaskType taskType, bool completed)
    {
        switch (taskType)
        {
            case TaskType.PieceInBox:
                {
                    PieceInBox(taskType, completed);
                    break;
                }
            case TaskType.NoSafetyMat:
                {
                    NoSafetyMat(taskType, completed);
                    break;
                }
            case TaskType.NoLightRays:
                {
                    NoLightRays(taskType, completed);
                    break;
                }
            case TaskType.InterruptorManual:
                {
                    InterruptorManual(taskType, completed);
                    break;
                }
            case TaskType.CycleHoldPressed:
                {
                    CycleHold(taskType, completed);
                    break;
                }
            case TaskType.EmergencyPressed:
                {
                    Emergency(taskType, completed);
                    break;
                }
            case TaskType.CycleStartPressed:
                {
                    CycleStart(taskType, completed);
                    break;
                }
            case TaskType.ResetPressed:
                {
                    Reset(taskType, completed);
                    break;
                }
            default:
                {
                    Pass(taskType, completed);
                    break;
                }
        }
    }

    private void Reset(TaskType taskType, bool completed)
    {
        if (completed)
        {
            bool fail = false;
            bool wasDoorClosed = checklist.GetTask(TaskType.DoorClosed).completed;
            bool wasPiecePlaced = checklist.GetTask(TaskType.PiecePlaced).completed;

            if (!wasDoorClosed)
            {
                Error("La puerta estaba abierta al presionar Reset.");
                fail = true;
            }
            if (!wasPiecePlaced)
            {
                Error("La pieza no estaba colocada en la mesa apropiada al presionar Reset.");
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

    private void CycleStart(TaskType taskType, bool completed)
    {
        if (completed)
        {
            bool wasResetPressed = checklist.GetTask(TaskType.ResetPressed).completed;
            bool wasAutomaticInterruptor = checklist.GetTask(TaskType.InterruptorAutomatic).completed;
            bool wasPiecePlaced = checklist.GetTask(TaskType.PiecePlaced).completed;
            bool wasDoorClosed = checklist.GetTask(TaskType.DoorClosed).completed;
            bool fail = false;

            if (!wasResetPressed)
            {
                Error("Presiono Cycle Start sin haber pressionado Reset.");
            }

            if (!wasAutomaticInterruptor)
            {
                Error("El interruptor no estaba en modo Auto al pressionar Cycle Start.");
                fail = true;
            }

            if (!wasDoorClosed)
            {
                Error("La puerta esta abierta al presionar Cycle Start.");
                fail = true;
            }

            if (!wasPiecePlaced)
            {
                Error("La pieza no estaba colocada en la mesa apropiada al presionar CycleStart");
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

    private void Emergency(TaskType taskType, bool completed)
    {
        if (completed)
        {
            Error("Presiono el boton Emergency.");
            Fail();
        }
        else
            Pass(taskType, completed);
    }

    private void CycleHold(TaskType taskType, bool completed)
    {
        if (completed)
        {
            Error("Presiono el boton Cycle Hold.");
            Fail();
        }
        else
            Pass(taskType, completed);
    }

    private void InterruptorManual(TaskType taskType, bool completed)
    {
        if (completed)
        {
            Error("Puso el interruptor en modo Manual");
            Fail();
        }
        else
            Pass(taskType, completed);
    }

    private void NoLightRays(TaskType taskType, bool completed)
    {
        if (!completed)
        {
            Error("Presencia de operador en cortina de luz");
            Fail();
        }
        else
            Pass(taskType, completed);
    }

    private void NoSafetyMat(TaskType taskType, bool completed)
    {
        if (!completed)
        {
            bool cycleStartPressed = checklist.GetTask(TaskType.CycleStartPressed).completed;
            if (cycleStartPressed)
            {
                Error("Piso tapete de seguridad ya que arranco el robot.");
                Fail();
            }
            else
                Pass(taskType, completed);
        }
        Pass(taskType, completed);
    }

    void PieceInBox(TaskType taskType, bool completed)
    {
        if (completed)
        {
            Pass(taskType, completed);
            Success();
        }
        else
            Pass(taskType, completed);
    }
}
