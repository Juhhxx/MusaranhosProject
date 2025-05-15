using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GraphManager : MonoBehaviour
{
    [SerializeField] private GraphPoint _startPoint;
    [SerializeField] private float _pointDistance;
    [SerializeField] private bool _drawConnections;
    [ReadOnly][SerializeField] private List<GraphDebugger> _points;

    private Dictionary<int,GraphPoint> _graph;
    public IDictionary<int,GraphPoint> Graph => _graph;
    private Stack<GraphPoint> _workingPoints;
    private int _pointNumber;

    [Serializable]
    public class GraphDebugger
    {
        public GraphDebugger(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public int ID;
        public string Name;
    }

    private void Awake()
    {
        _graph = new Dictionary<int, GraphPoint>();
        _points = new List<GraphDebugger>();

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
        if (_graph.ContainsValue(point)) return;
        Debug.Log($"Register Point {_pointNumber}");

        point.SetID(_pointNumber);
        point.SetConnections(GetConnections(point));

        _graph.Add(_pointNumber, point);
        _points.Add(new GraphDebugger(_pointNumber, point.transform.parent.gameObject.name));

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
                Debug.DrawLine(point.Value.transform.position, p.transform.position, Color.cyan);
            }
        }
    }

}
