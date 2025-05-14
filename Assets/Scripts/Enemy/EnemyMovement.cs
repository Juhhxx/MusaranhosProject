using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveStep;
    [SerializeField] private float _moveInterval;

    // Look Parameters 
    private Transform _playerTrans;
    private Vector3 _lookTarget;

    // Pathfinding Parameters
    private Vector3 _moveTarget;
    private Vector3 _navMeshPath;
    private Vector3 _moveVector;
    private NavMeshAgent _agent;
    
    private void Start()
    {
        _playerTrans = Detector.GetClosestInArea<PlayerMovement>
        (transform, 100, LayerMask.NameToLayer("Player")).transform;
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        _lookTarget     = _playerTrans.position;
        _lookTarget.y   = transform.position.y;

        transform.LookAt(_lookTarget);
    }
    
    private void Move()
    {
    }
}
