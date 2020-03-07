public static class ChecklistFactory
{
    private static ManualChecklist manualChecklist;
    private static AutomaticChecklist automaticChecklist;

    public static Checklist Get(SimulationMode mode)
    {
        switch (mode)
        {
            case SimulationMode.Automatic:
                {
                    return new AutomaticChecklist();
                }
            case SimulationMode.Manual:
                {
                    return new ManualChecklist();
                }
            default:
                {
                    return new AutomaticChecklist();
                }
        }
    }

    public static ChecklistValidator GetValidator(SimulationMode mode, Checklist checklist)
    {
        switch (mode)
        {
            case SimulationMode.Automatic:
                {
                    return new AutomaticChecklistValidator(checklist);
                }
            case SimulationMode.Manual:
                {
                    return new ManualChecklistValidator(checklist);
                }
            default:
                {
                    return new AutomaticChecklistValidator(checklist);
                }
        }

    }

}