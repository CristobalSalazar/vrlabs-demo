using System.Collections.Generic;
using System.Linq;

public class RequestPostBody
{
    public string nombreDeOperador;
    public string modoDeSimulacion;
    public bool simulacionCompletada;
    public int intentos;
    public string[] actividadesCompletadas;
    public string[] errores;
    public float segundos;
    public float segundosTotal;
    public int erroresTotal;

    public static RequestPostBody CreateFromSessionAndAttempt(SimulationSession session, SimulationAttempt attempt)
    {
        RequestPostBody body = new RequestPostBody();
        body.nombreDeOperador = session.operatorName;
        body.modoDeSimulacion = session.simulationMode.ToString();
        body.intentos = session.totalAttempts;
        body.simulacionCompletada = attempt.simulationComplete;
        body.actividadesCompletadas = ConvertTasksToStringArray(attempt.checklist.GetCompletedTasks());
        body.errores = attempt.errors.ToArray();
        body.erroresTotal = session.totalErrors;
        body.segundos = attempt.GetTime();
        body.segundosTotal = session.totalSeconds;
        return body;
    }

    private static string[] ConvertTasksToStringArray(IEnumerable<Task> tasks)
    {
        return tasks.Select(x => x.description).ToArray();
    }
}