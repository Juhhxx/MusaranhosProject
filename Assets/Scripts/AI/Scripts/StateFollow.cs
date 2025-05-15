using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "StateFollow", menuName = "State Machines/States/StateFollow")]
public class StateFollow : StateAbstract
{
    GameObject gameObject;

    EnemyMovement _movement;

    protected override void EntryAction()
    {
        Debug.Log("Entering Follow");

        _movement.GridBased = false;
    }

    protected override void StateAction()
    {
        Debug.Log("State Follow");
    }

    protected override void ExitAction()
    {
        Debug.Log("Exit Follow");

        _movement.GridBased = true;

    }
    public override void InstantiateState()
    {
        gameObject = base.objectReference;

        _movement = GetComponent<EnemyMovement>(gameObject);

        base.state = new State(base.Name, EntryAction, StateAction, ExitAction);
    }
}
