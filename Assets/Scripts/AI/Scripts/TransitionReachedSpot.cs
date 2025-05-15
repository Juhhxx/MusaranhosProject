using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionReachedSpot", menuName = "State Machines/Transitions/TransitionReachedSpot")]
public class TransitionReachedSpot : TransitionAbstract
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