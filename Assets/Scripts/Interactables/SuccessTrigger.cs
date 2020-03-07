using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Piece")
        {
            SimulationAttempt.SetTask(TaskType.PieceInBox, true);
        }
    }
}