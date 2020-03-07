using UnityEngine;
using UnityEngine.UI;

public class PlacementController : MonoBehaviour, Interactable
{
    public string description { get; set; }
    public bool canInteract { get; set; }
    public Piece piece;
    public static PlacementController main;
    public bool canTake = false;

    public void Interact()
    {
        piece.Place();
        SimulationAttempt.SetTask(TaskType.PiecePlaced, true);
        canInteract = false;
        canTake = true;
    }
    void Awake()
    {
        main = this;
    }

    void Start()
    {
        description = "Colocar";
        canTake = false;
    }
}