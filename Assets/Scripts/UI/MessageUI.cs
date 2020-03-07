using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Text))]
public class MessageUI : MonoBehaviour
{
    private static MessageUI main;
    private Text _text;
    private Animator _animator;

    void Awake() {
        main = this;
    }

    void Start() {
        _animator = GetComponent<Animator>();
        _text = GetComponent<Text>();
    }

    public static void DisplayMessage(string message)
    {
        main._text.text = message;
        main._animator.Play("DisplayMessage", 0, 0);
    }
}
