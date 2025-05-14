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

    // Look Parameters 
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
    
    private void Start()
    {
        _agent                  = GetComponent<NavMeshAgent>();
        _pathfinder             = GetComponent<Pathfinder>();

        _graph                  = _graphManager.Graph;
        _currentPoint           = _startPoint;
        _agent.updateRotation   = false;
        _moveTarget             = _startPoint.GetPosition();

        transform.position = _currentPoint.GetPosition();
        SetNewDestination(_destinationPoint);
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
        CountTimer();

        if (_moveTimer == 0) GetNextPoint();
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
    private void SetNewDestination(GraphPoint destination)
    {
        _currentPath = _pathfinder.GetPath(_currentPoint, destination);
    }
    private void GetNextPoint()
    {
        if (Vector3.Distance(_moveTarget, transform.position) < 0.5f || _moveTarget == _playerTrans.position)
        {
            _currentPoint   = _currentPath.Pop();
            _moveTarget     = _currentPoint.GetPosition();
        }
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
