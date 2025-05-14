using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveInterval;

    // Look Parameters 
    private Transform _playerTrans;
    private Vector3 _lookTarget;

    // Pathfinding Parameters
    [SerializeField] private GraphManager _graphManager;
    private IDictionary<int,GraphPoint> _graph;
    [SerializeField] private GraphPoint _startPoint;
    [SerializeField] private GraphPoint _destinationPoint;
    private Pathfinder _pathfinder;
    private Stack<GraphPoint> _currentPath;
    private GraphPoint _currentPoint;

    private Vector3 _moveTarget;
    private NavMeshAgent _agent;
    private YieldInstruction _wfs;
    
    private void Start()
    {
        // _playerTrans    = Detector.GetClosestInArea<PlayerMovement>
        //                     (transform, 100, 0).transform;
        _agent          = GetComponent<NavMeshAgent>();
        _pathfinder     = GetComponent<Pathfinder>();
        _wfs            = new WaitForSeconds(_moveInterval);

        _graph = _graphManager.Graph;
        _currentPoint = _startPoint;

        transform.position = _currentPoint.GetPosition();
        SetNewDestination(_destinationPoint);

        StartCoroutine(GridMovement());
    }
    private void Update()
    {
        Move();
    }

    private void LookAtTarget()
    {
        _lookTarget     = _playerTrans.position;
        _lookTarget.y   = transform.position.y;

        transform.LookAt(_lookTarget);
    }

    private IEnumerator GridMovement()
    {
        while (_currentPath.Count > 0)
        {
            GetNextPoint();

            yield return _wfs;
        }
    }
    private void SetNewDestination(GraphPoint destination)
    {
        _currentPath = _pathfinder.GetPath(_currentPoint, destination);
    }
    private void GetNextPoint()
    {
        if (Vector3.Distance(_moveTarget, transform.position) < 0.1f)
        {
            _currentPoint   = _currentPath.Pop();
            _moveTarget     = _currentPoint.GetPosition();
        }
    }
    private void Move()
    {
        _agent.destination = _moveTarget;
    }
}
