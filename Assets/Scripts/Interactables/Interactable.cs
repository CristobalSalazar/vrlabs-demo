public interface Interactable
{
    string description { get; set; }
    bool canInteract { get; set; }
    void Interact();
}
