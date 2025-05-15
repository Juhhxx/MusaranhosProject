using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "StateFollowSound", menuName = "State Machines/States/StateFollowSound")]
public class StateFollowSound : StateAbstract
{
    GameObject gameObject;

    GraphManager _graph;
    EnemyMovement _movement;
    GraphPoint _target;

    protected override void EntryAction()
    {
        Debug.Log("Entering Empty");

        _target = _graph.PlayerPoint;
        _movement.SetNewDestination(_target);
    }

    protected override void StateAction()
    {
        Debug.Log("State Empty");
    }

    protected override void ExitAction()
    {
        Debug.Log("Exit Empty");
    }
    public override void InstantiateState()
    {
        gameObject = base.objectReference;

        _graph = GetComponent<GraphManager>(FindObjectByType<GraphManager>());
        _movement = GetComponent<EnemyMovement>(gameObject);

        base.state = new State(base.Name, EntryAction, StateAction, ExitAction);
    }
}
