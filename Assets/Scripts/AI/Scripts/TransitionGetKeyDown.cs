using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionGetKeyDown", menuName = "State Machines/Transitions/TransitionGetKeyDown")]
public class TransitionGetKeyDown : TransitionAbstract
{
    [SerializeField] private KeyCode key;
    protected override void Action()
    {
        Debug.Log($"{key} was pressed!");
    }
    protected override bool Condition()
    {
        return Input.GetKeyDown(key);
    }
    public override void InstantiateTransition()
    {
        base.transition = new Transition(base.Name, Condition, base.ToState.State, Action);
    }
}