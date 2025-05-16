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
    private Animator _anim;

    private int _dangerLevel = 0;
    public int DangerLevel => _dangerLevel;
    public event EventHandler OnAttack;

    private void Start()
    {
        _agent      = GetComponent<NavMeshAgent>();
        _anim       = GetComponent<Animator>();
        _flashed    = false;
        _shived     = false;
        _hearSound  = false;
    }

    private void Update()
    {
        if (_dangerLevel > 0 && !_agent.enabled) _agent.enabled = true;
        else if (_dangerLevel == 0) _agent.enabled = false;

        _anim.SetFloat("Speed", Mathf.Abs(_agent.velocity.magnitude));
    }

    public void Flashed(bool value) => _flashed = value;
    public void Shived(bool value) => _shived = value;
    public void HearSound(bool value) => _hearSound = value;
    public void Attack() => OnAttack?.Invoke(this, null);
    
    public void SetDangerLevel(int value) => _dangerLevel = value;
}
