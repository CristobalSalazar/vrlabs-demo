using UnityEngine;

public class Piece : MonoBehaviour, Interactable
{
    [SerializeField] private Transform startTransform;

    public string description { get; set; }
    public bool canInteract { get; set; }
    public static bool isHeld = false;

    private Vector3 _initPosition;
    private Transform _initTransform;
    private Quaternion _initRotation;

    private void Start()
    {
        canInteract = true;
        _initPosition = transform.position;
        _initTransform = transform.parent;
        _initRotation = transform.rotation;
        description = "Tomar";

        if (startTransform != null)
        {
            transform.SetParent(startTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

    public void Interact()
    {
        PlayerHold();
    }

    private void PlayerHold()
    {
        canInteract = false;
        isHeld = true;
        Transform player = Camera.main.transform;
        transform.SetParent(player);
        transform.localPosition = (Vector3.forward / 2 - Vector3.up / 3);
        transform.localEulerAngles = new Vector3(90, 0, 0);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        PlacementController.main.canInteract = true;
        PlacementController.main.canTake = false;
        DoorController.main.canInteract = false;
    }

    public void Place()
    {
        isHeld = false;
        canInteract = true;
        transform.position = _initPosition;
        transform.SetParent(_initTransform);
        transform.rotation = _initRotation;
        DoorController.main.canInteract = true;
    }

}
