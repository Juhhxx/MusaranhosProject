using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveInterval;
    private float _moveTimer;
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private bool _gridBased;
    public bool GridBased { get => _gridBased; set => _gridBased = value; }

    // Look Parameters 
    private Vector3 _lookTarget;

    // Pathfinding Parameters
    [SerializeField] private GraphManager _graphManager;
    private IDictionary<int,GraphPoint> _graph;
    [SerializeField] private GraphPoint _startPoint;
    public GraphPoint StartPoint => _startPoint;
    private Pathfinder _pathfinder;
    private Stack<GraphPoint> _currentPath;
    private GraphPoint _currentPoint;
    public GraphPoint CurrentPoint => _currentPoint;

    private Vector3 _moveTarget;
    private NavMeshAgent _agent;
    
    private void Start()
    {
        _agent                  = GetComponent<NavMeshAgent>();
        _pathfinder             = GetComponent<Pathfinder>();
        _graph                  = _graphManager.Graph;
        _currentPath            = new Stack<GraphPoint>();
        _agent.updateRotation   = false;
        _moveTimer              = 0f;

        GridTeleport(_startPoint);
    }
    private void Update()
    {
        if (_gridBased) GridMovement();
        else            FollowMovement();

        LookAtTarget();
        Move();
    }

    private void LookAtTarget()
    {
        if (_playerTrans == null) return;

        _lookTarget     = _playerTrans.position;
        _lookTarget.y   = transform.position.y;

        transform.LookAt(_lookTarget);
    }

    private void GridMovement()
    {
        if (_currentPath.Count == 0) return;

        if (_moveTimer == 0) GetNextPoint();
        CountTimer();
    }
    private void CountTimer()
    {
        if (_moveTimer < _moveInterval)
        {
            _moveTimer += Time.fixedDeltaTime;
        }
        else if (_moveTimer >= _moveInterval)
        {
            ResetTimer();
        }
    }
    private void ResetTimer()
    {
        _moveTimer = 0f;
    }
    public void SetNewDestination(GraphPoint destination)
    {
        _currentPath = _pathfinder.GetPath(_currentPoint, destination);
        Debug.Log($"Set Destination from {_currentPoint.transform.parent.name} to {destination.transform.parent.name}");
        Debug.Log($"Current Path Size = {_currentPath.Count}");
    }
    private void GetNextPoint()
    {
        if (Vector3.Distance(_moveTarget, transform.position) < 0.5f || _moveTarget == _playerTrans.position)
        {
            _currentPoint   = _currentPath.Pop();
            _moveTarget     = _currentPoint.GetPosition();
        }
    }
    public void GridTeleport(GraphPoint point)
    {
        _agent.enabled = false;
        transform.position = point.GetPosition();
        _moveTarget = point.GetPosition();
        _currentPoint = point;
        _agent.enabled = true;
    }
    public void ResetPath()
    {
        _currentPath = new Stack<GraphPoint>();
        ResetTimer();
    }

    private void FollowMovement()
    {
        ResetTimer();
        _moveTarget = _playerTrans.position;
    }

    private void Move()
    {
        _agent.destination = _moveTarget;
    }
}
