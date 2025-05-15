using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionShived", menuName = "State Machines/TransitionShived")]
public class TransitionShived : TransitionAbstract
{
    protected override void Action()
    {
        Debug.Log($"Transition Passed");
    }
    protected override bool Condition()
    {
        return true;
    }
    public override void InstantiateTransition()
    {
        base.transition = new Transition(base.Name, Condition, base.ToState.State, Action);
    }
}