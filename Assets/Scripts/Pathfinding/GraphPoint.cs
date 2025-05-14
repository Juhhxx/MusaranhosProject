using System.Collections.Generic;
using UnityEngine;

public class GraphPoint : MonoBehaviour
{
    [SerializeField] private int _pointID;
    [SerializeField] private List<GraphPoint> _connections;
    public IEnumerable<GraphPoint> Connections => _connections;

    public void SetID(int id) => _pointID = id;
    public void SetConnections(List<GraphPoint> connections) => 
    _connections = new List<GraphPoint>(connections);
}
