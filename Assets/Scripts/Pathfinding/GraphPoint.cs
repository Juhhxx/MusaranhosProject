using System.Collections.Generic;
using UnityEngine;

public class GraphPoint : MonoBehaviour
{
    [SerializeField] private int _pointID;
    public int ID => _pointID;
    [SerializeField] private List<GraphPoint> _connections;
    public IEnumerable<GraphPoint> Connections => _connections;

    private GraphManager _graph;

    public void SetGraph(GraphManager graph) => _graph = graph;
    public void SetID(int id) => _pointID = id;
    public void SetConnections(List<GraphPoint> connections) => 
    _connections = new List<GraphPoint>(connections);
    public Vector3 GetPosition()
    {
        Vector3 position = transform.position;
        position.y = 0;

        return position;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.gameObject.GetComponent<PlayerController>();

        if (p != null)
        {
            _graph.SetPlayerPoint(_pointID);
        }
    }
}
