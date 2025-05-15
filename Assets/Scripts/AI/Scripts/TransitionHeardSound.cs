using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionHeardSound", menuName = "State Machines/Transitions/TransitionHeardSound")]
public class TransitionHeardSound : TransitionAbstract
{
    GameObject gameObject;

    EnemyController _enemyController;

    protected override void Action()
    {
        Debug.Log($"Transition HeardSound");

        _enemyController.HearSound(false);
    }
    protected override bool Condition()
    {
        return _enemyController.GetHearSound;
    }
    public override void InstantiateTransition()
    {
        gameObject = base.objectReference;

        _enemyController = GetComponent<EnemyController>(gameObject);

        base.transition = new Transition(base.Name, Condition, base.ToState.State, Action);
    }
}