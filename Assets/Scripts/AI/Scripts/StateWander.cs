using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "StateWander", menuName = "State Machines/States/StateWander")]
public class StateWander : StateAbstract
{
    GameObject gameObject;
    Transform transform;

    GraphManager _graph;
    EnemyMovement _movement;
    GraphPoint _target;

    protected override void EntryAction()
    {
        Debug.Log("Entering Wander");

        GetNewPath();
    }

    protected override void StateAction()
    {
        Debug.Log("State Wander");
        Debug.Log(_target?.transform.position);

        if (Vector3.Distance(transform.position, _target.transform.position) <= 0.5f)
        {
            GetNewPath();
        }
        DrawCross(_target.transform.position, 2, Color.red);
    }

    protected override void ExitAction()
    {
        Debug.Log("Exit Wander");
    }
    public override void InstantiateState()
    {
        gameObject = base.objectReference;
        transform = gameObject.transform;

        _graph = GetComponent<GraphManager>(FindObjectByType<GraphManager>());
        _movement = GetComponent<EnemyMovement>(gameObject);

        base.state = new State(base.Name, EntryAction, StateAction, ExitAction);
    }

    private void ChooseRandomPoint()
    {
        GraphPoint point = _movement.CurrentPoint;

        int nextPoint = Random.Range(0, point.Connections.Count());


        _target = point.Connections.ToList()[nextPoint];

        Debug.LogWarning($"Choose {_target.transform.parent.name} from point {point.transform.parent.name}");
    }
    private void GetNewPath()
    {
        Debug.Log("CHOOSE NEW POINT");
        ChooseRandomPoint();
        _movement.SetNewDestination(_target);
    }
    private void DrawCross(Vector3 position, float size, Color color)
    {
        Vector3 bgnX = position + (Vector3.right * (size/2));
        Vector3 endX = position + (Vector3.left * (size/2));

        Debug.DrawLine(bgnX, endX, color);

        Vector3 bgnY = position + (Vector3.up * (size/2));
        Vector3 endY = position + (Vector3.down * (size/2));

        Debug.DrawLine(bgnY, endY, color);

        Vector3 bgnZ = position + (Vector3.forward * (size/2));
        Vector3 endZ = position + (Vector3.back * (size/2));

        Debug.DrawLine(bgnZ, endZ, color);
    }
}
