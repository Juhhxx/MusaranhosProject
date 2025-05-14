using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    [SerializeField] private GraphPoint _startPoint;
    [SerializeField] private float _pointDistance;
    [SerializeField] private bool _drawConnections;


    private Dictionary<int,GraphPoint> _graph;
    public IDictionary<int,GraphPoint> Graph => _graph;
    private Stack<GraphPoint> _workingPoints;
    private int _pointNumber;

    private void Awake()
    {
        _graph = new Dictionary<int, GraphPoint>();

        CreateGraph();
    }
    private void Update()
    {
        if (_drawConnections) DrawConnections();
    }

    private void CreateGraph()
    {
        Debug.Log("Start Creating Graph");
        _workingPoints = new Stack<GraphPoint>();

        RegisterPoint(_startPoint);

        while (_workingPoints.Count != 0)
        {
            RegisterPoint(_workingPoints.Pop());
        }
    }
    private void RegisterPoint(GraphPoint point)
    {
        Debug.Log($"Register Point {_pointNumber}");

        if (_graph.ContainsValue(point)) return;

        point.SetID(_pointNumber);
        point.SetConnections(GetConnections(point));

        _graph.Add(_pointNumber, point);

        _pointNumber++;
    }
    private List<GraphPoint> GetConnections(GraphPoint point)
    {
        Debug.Log($"Get Connections for Point {_pointNumber}");
        List<GraphPoint> connections = new List<GraphPoint>();

        for (int i = 0; i < 4; i++)
        {
            point.transform.Rotate(0f, 90f, 0f);

            if (Physics.Raycast(point.transform.position + (point.transform.forward * 1.5f), 
                                point.transform.forward, 
                                out RaycastHit hit, _pointDistance - 1.5f))
            {
                GraphPoint p = hit.collider.GetComponent<GraphPoint>();

                if (p == null) continue;

                connections.Add(p);
                _workingPoints.Push(p);

                Debug.Log($"Connection : {hit.collider.gameObject.name}");
            }
        }

        Debug.Log($"{connections.Count} Connections Found!");
        return connections;
    }
    private void DrawConnections()
    {
        foreach (var point in _graph)
        {
            foreach (GraphPoint p in point.Value.Connections)
            {
                Debug.DrawLine(point.Value.transform.position, p.transform.position, Color.red);
            }
        }
    }

}
