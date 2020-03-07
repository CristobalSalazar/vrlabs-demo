using UnityEngine;
using UnityEngine.UI;

public class InteractiveRaycaster : MonoBehaviour
{
    [SerializeField] private GameObject interactiveTextObject;
    private bool _canInteract;
    private Text interactiveText;

    private void Start()
    {
        SimulationEvents.On("SimulationSuccess", SetInteractiveStateOff);
        SimulationEvents.On("SimulationFailed", SetInteractiveStateOff);
        interactiveText = interactiveTextObject.GetComponent<Text>();
        _canInteract = true;
    }

    void OnDestroy()
    {
        SimulationEvents.Unsubscribe("SimulationSuccess", SetInteractiveStateOff);
        SimulationEvents.Unsubscribe("SumulationFailed", SetInteractiveStateOff);
    }

    private void SetInteractiveStateOff()
    {
        _canInteract = false;
    }

    private void Update()
    {
        if (!_canInteract) return;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1.25f);
        interactiveTextObject.SetActive(false);

        foreach (RaycastHit hit in hits)
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null && interactable.canInteract)
            {
                interactiveTextObject.SetActive(true);
                interactiveText.text = interactable.description;
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.Interact();
                }
                break;
            }
        }
    }
}
