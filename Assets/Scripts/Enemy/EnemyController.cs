using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool _flashed;
    private bool _shived;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _flashed = false;
        _shived = false;
    }

    public void Flashed() => _flashed = true;
    public void Shived() => _shived = true;
}
