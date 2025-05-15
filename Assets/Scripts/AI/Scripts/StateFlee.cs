using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "StateFlee", menuName = "State Machines/States/StateFlee")]
public class StateFlee : StateAbstract
{
    GameObject gameObject;

    GraphManager _graph;
    EnemyMovement _movement;

    protected override void EntryAction()
    {
        Debug.Log("Entering Flee");
        
        _movement.GridTeleport(_graph.GetPointAwayFromPlayer());
        _movement.ResetPath();
    }

    protected override void StateAction()
    {
        Debug.Log("State Flee");
    }

    protected override void ExitAction()
    {
        Debug.Log("Exit Flee");
    }
    public override void InstantiateState()
    {
        gameObject = FindObjectByType<EnemyMovement>();

        _graph = GetComponent<GraphManager>(FindObjectByType<GraphManager>());
        _movement = GetComponent<EnemyMovement>(gameObject);

        base.state = new State(base.Name, EntryAction, StateAction, ExitAction);
    }
}
