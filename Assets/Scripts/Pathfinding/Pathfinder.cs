using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public class Node
    {
        public Node(GraphPoint point)
        {
            Point = point;
        }
        public GraphPoint Point;
        public Node FromNode;
        public float CostSoFar;
        public float TotalCost;
    }

    [SerializeField] private bool _showPath = true;
    private List<GraphPoint> _currentPath;

    private void Update()
    {
        if (_showPath) ShowPath();
    }

    public Stack<GraphPoint> GetPath(GraphPoint start, GraphPoint end)
    {
        Node s = new Node(start);
        Node e = new Node(end);

        Stack<GraphPoint> path = AStar(s, e);

        _currentPath = new List<GraphPoint>(path);

        return path;
    }
    private Stack<GraphPoint> AStar(Node start, Node end)
    {
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        Node currentPoint = null;

        start.TotalCost = 0;
        start.FromNode = null;
        start.TotalCost = HeuristicCalculation(start, end);
        open.Add(start);

        while (open.Count > 0)
        {
            currentPoint = GetCheapestNode(open);
            if (currentPoint == end) break;

            foreach (GraphPoint c in currentPoint.Point.Connections)
            {
                Node next;

                if (c != end.Point) next = new Node(c);
                else next = end;

                float toNodeCost = currentPoint.CostSoFar + 1;

                if (closed.Contains(next))
                {
                    if (next.CostSoFar <= toNodeCost) continue;
                    closed.Remove(next);
                    open.Add(next);
                }
                else if (open.Contains(next))
                {
                    if (next.CostSoFar <= toNodeCost) continue;
                }

                next.CostSoFar = toNodeCost;
                next.FromNode = currentPoint;
                next.TotalCost = HeuristicCalculation(next, end) + toNodeCost;
                
                if (!open.Contains(next)) open.Add(next);

                open.Remove(currentPoint);
                closed.Add(currentPoint);
            }
        }
        
        if (currentPoint != end) return null;
        else
        {
            Stack<GraphPoint> path = new Stack<GraphPoint>();

            while (currentPoint != start)
            {
                path.Push(currentPoint.Point);

                currentPoint = currentPoint.FromNode;
            }
            
            path.Push(currentPoint.Point);

            return path;
        }
    }
    // private Stack<GraphPoint> Drisjka(Node start, Node end)
    // {

    // }
    private Node GetCheapestNode(List<Node> list)
    {
        Node result = null;

        foreach (Node p in list)
        {
            if (result == null) result = p;
            else
            {
                if (p.TotalCost < result.TotalCost) result = p;
            }
        }

        return result;
    }
    private float HeuristicCalculation(Node point, Node end)
    {
        return  Mathf.Abs(point.Point.transform.position.x - end.Point.transform.position.x) + 
                Mathf.Abs(point.Point.transform.position.y - end.Point.transform.position.y);
    }

    private void ShowPath()
    {
        for (int i = 1; i < _currentPath.Count; i++)
        {
            Vector3 curP = _currentPath[i].transform.position;
            Vector3 lastP = _currentPath[i - 1].transform.position;

            Debug.DrawLine(curP, lastP, Color.red);
        }
    }
}
