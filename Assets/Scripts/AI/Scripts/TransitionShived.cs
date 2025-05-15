using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionShived", menuName = "State Machines/Transitions/TransitionShived")]
public class TransitionShived : TransitionAbstract
{
    GameObject gameObject;

    EnemyController _enemyController;

    protected override void Action()
    {
        Debug.Log($"Transition Shived");

        _enemyController.Shived(false);
    }
    protected override bool Condition()
    {
        return _enemyController.GetShived;
    }
    public override void InstantiateTransition()
    {
        gameObject = base.objectReference;

        _enemyController = GetComponent<EnemyController>(gameObject);

        base.transition = new Transition(base.Name, Condition, base.ToState.State, Action);
    }
}