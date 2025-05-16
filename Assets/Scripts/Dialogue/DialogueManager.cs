using UnityEngine;
using Yarn;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner _runner;
    [SerializeField] private KeyCode _key;
    private LineView _lineView;

    private void Start()
    {
        _lineView = GetComponentInChildren<LineView>();
        StartDialogue("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(_key)) _lineView.OnContinueClicked();
    }

    public void StartDialogue(string dialogue)
    {
        _runner.StartDialogue(dialogue);
    }

}
