using UnityEngine;
using System.Collections;

public class RobotAnimation : MonoBehaviour
{
    public Transform robotHead;
    public Conveyor conveyor;
    private Rigidbody pieceRb;
    private Piece piece;
    private BoxCollider pieceCollider;
    private Animator animator;
    private bool animationIsPlaying;
    private AudioClip clip;
    private AudioSource audioSource;
    private const float AUDIO_VOLUME = 0.25f;
    private const string AUDIO_FILE = "Sounds/robot";

    void Start()
    {
        DisableAnimator();
        SetupAudio();
        SubscribeToEvents();
    }

    void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        SimulationAttempt.Checklist.OnTaskChanged.AddListener(CheckAnimationConditions);
        SimulationEvents.On(EventType.SimulationFailed, StopOperations);
    }

    void UnsubscribeToEvents()
    {
        SimulationAttempt.Checklist.OnTaskChanged.RemoveListener(CheckAnimationConditions);
        SimulationEvents.Unsubscribe(EventType.SimulationFailed, StopOperations);
    }

    void DisableAnimator()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    void SetupAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        clip = Resources.Load<AudioClip>(AUDIO_FILE);
        audioSource.clip = clip;
        audioSource.volume = AUDIO_VOLUME;
    }

    void CheckAnimationConditions(TaskType type, Task t)
    {
        if (CanStartAnimation())
        {
            PlayAnimation();
        }
    }

    bool CanStartAnimation()
    {
        bool wasStartPressed = SimulationAttempt.Checklist.GetTask(TaskType.CycleStartPressed).completed;
        bool wasDoorClosed = SimulationAttempt.Checklist.GetTask(TaskType.DoorClosed).completed;
        bool wasPiecePlaced = SimulationAttempt.Checklist.GetTask(TaskType.PiecePlaced).completed;
        bool wasAutomaticMode = SimulationAttempt.Checklist.GetTask(TaskType.InterruptorAutomatic).completed;
        bool noSafetyMat = SimulationAttempt.Checklist.GetTask(TaskType.NoSafetyMat).completed;

        bool[] conditions = { wasStartPressed, wasDoorClosed, wasPiecePlaced, wasAutomaticMode, noSafetyMat };

        foreach (bool condition in conditions)
        {
            if (!condition)
            {
                return false;
            }
        }
        return true;
    }

    public void StopOperations()
    {
        animator.speed = 0;
        audioSource.Stop();
        conveyor.SetOffState();
    }


    public void PlayAnimation()
    {
        if (animator.enabled == false)
        {
            audioSource.Play();
            animator.enabled = true;
        }
        else
        {
            animator.Play("Pickup");
        }
        animationIsPlaying = true;
    }

    public void PauseAnimation()
    {
        animator.speed = 0;
    }

    void SetPiecePhysics()
    {
        pieceRb = piece.GetComponent<Rigidbody>();
        pieceCollider = piece.GetComponent<BoxCollider>();
        pieceRb.isKinematic = true;
        pieceRb.useGravity = true;
    }

    public void PickupPiece()
    {
        piece = PlacementController.main.piece;
        if (piece == null) return;
        SetPiecePhysics();
        piece.transform.SetParent(robotHead);
    }

    public void DropPiece()
    {
        if (piece == null) return;
        StartCoroutine(FadeOutRoutine());
        piece.transform.SetParent(null);
        pieceRb.isKinematic = false;
        pieceRb.AddForce(pieceRb.transform.right * 3, ForceMode.Impulse);
        piece = null;
    }

    private IEnumerator FadeOutRoutine()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / 20;
            yield return null;
        }
    }
}
