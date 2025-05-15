using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionReachedSpot", menuName = "State Machines/Transitions/TransitionReachedSpot")]
public class TransitionReachedSpot : TransitionAbstract
{
    GameObject gameObject;

    EnemyMovement _movement;

    protected override void Action()
    {
        Debug.Log($"Transition Passed");
    }
    protected override bool Condition()
    {
        return _movement.AtTarget;
    }
    public override void InstantiateTransition()
    {
        gameObject = base.objectReference;

        _movement = GetComponent<EnemyMovement>(gameObject);

        base.transition = new Transition(base.Name, Condition, base.ToState.State, Action);
    }
}