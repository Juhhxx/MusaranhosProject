using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionFlashed", menuName = "State Machines/Transitions/TransitionFlashed")]
public class TransitionFlashed : TransitionAbstract
{
    GameObject gameObject;

    EnemyController _enemyController;

    protected override void Action()
    {
        Debug.Log($"Transition Flashed");

        _enemyController.Flashed(false);
    }
    protected override bool Condition()
    {
        return _enemyController.GetFlashed;
    }
    public override void InstantiateTransition()
    {
        gameObject = base.objectReference;

        _enemyController = GetComponent<EnemyController>(gameObject);

        base.transition = new Transition(base.Name, Condition, base.ToState.State, Action);
    }
}