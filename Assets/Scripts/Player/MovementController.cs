using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool _canMove;
    [SerializeField] private float movementSpeed = 2;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SimulationEvents.On(EventType.SimulationStarted, AllowMovement);
        SimulationEvents.On(EventType.SimulationFailed, ProhibitMovement);
        SimulationEvents.On(EventType.SimulationSuccess, ProhibitMovement);
    }

    void Update()
    {
        if (!_canMove) return;
        Vector3 forwardTranslation = transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        Vector3 sideTranslation = transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        Vector3 translationVector = transform.position + (forwardTranslation + sideTranslation);
        agent.destination = translationVector;
    }

    void OnDestroy()
    {
        SimulationEvents.Unsubscribe(EventType.SimulationStarted, AllowMovement);
        SimulationEvents.Unsubscribe(EventType.SimulationFailed, ProhibitMovement);
        SimulationEvents.Unsubscribe(EventType.SimulationSuccess, ProhibitMovement);
    }

    void ProhibitMovement()
    {
        _canMove = false;
    }

    void AllowMovement()
    {
        _canMove = true;
    }


}