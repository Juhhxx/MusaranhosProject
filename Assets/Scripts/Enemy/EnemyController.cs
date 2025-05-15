using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool _flashed;
    public bool GetFlashed => _flashed; 
    private bool _shived;
    public bool GetShived => _shived;
    private NavMeshAgent _agent;
    public event EventHandler OnAttack;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _flashed = false;
        _shived = false;
    }

    public void Flashed(bool value) => _flashed = value;
    public void Shived(bool value) => _shived = value;
}
