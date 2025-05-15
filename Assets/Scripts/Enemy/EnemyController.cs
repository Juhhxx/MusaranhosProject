using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool _flashed;
    public bool GetFlashed => _flashed; 
    private bool _shived;
    public bool GetShived => _shived;
    private bool _hearSound;
    public bool GetHearSound => _hearSound;
    private NavMeshAgent _agent;

    private int dangerLevel;
    public int DangerLevel => dangerLevel;
    public event EventHandler OnAttack;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _flashed = false;
        _shived = false;
    }

    public void Flashed(bool value) => _flashed = value;
    public void Shived(bool value) => _shived = value;
    public void HearSound(bool value) => _hearSound = value;
    public void Attack() => OnAttack?.Invoke(this, null);
    
    public void SetDangerLevel(int value) => dangerLevel = value;
}
