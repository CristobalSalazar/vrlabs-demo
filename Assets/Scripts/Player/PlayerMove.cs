using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private PlayerLook playerLook;
    private bool _canMove;


    void Start()
    {
        SimulationEvents.On(EventType.SimulationStarted, AllowMovement);
        SimulationEvents.On(EventType.SimulationFailed, ProhibitMovement);
        SimulationEvents.On(EventType.SimulationSuccess, ProhibitMovement);
    }

    void OnDestroy()
    {
        SimulationEvents.Unsubscribe(EventType.SimulationStarted, AllowMovement);
        SimulationEvents.Unsubscribe(EventType.SimulationFailed, ProhibitMovement);
        SimulationEvents.Unsubscribe(EventType.SimulationSuccess, ProhibitMovement);
    }

    public void ProhibitMovement()
    {
        _canMove = false;
    }

    public void AllowMovement()
    {
        _canMove = true;
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            Vector3 forwardTranslation = Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
            Vector3 sideTranslation = Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
            Vector3 translationVector = forwardTranslation + sideTranslation;
            transform.Translate(translationVector);
        }
    }
}
