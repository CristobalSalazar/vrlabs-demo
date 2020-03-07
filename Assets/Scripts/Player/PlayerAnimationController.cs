using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EmitEvent(EventType e)
    {
        SimulationEvents.Emit(e);
    }

    public void GoToStart()
    {
        SimulationSession.Current.ResetSession();
        UnlockCursor();
        SceneManager.LoadScene("Start");
    }

}
