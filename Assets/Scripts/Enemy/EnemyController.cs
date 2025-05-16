using System;
using AI.FSMs.UnityIntegration;
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
    
    private StateMachineRunner _stateMachineRunner;
    public event EventHandler OnAttack;
    public event EventHandler OnLostChase;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _flashed = false;
        _shived = false;
        _hearSound = false;
        _stateMachineRunner = GetComponent<StateMachineRunner>();
    }

    private void Update()
    {
        if (_dangerLevel > 0 && !_stateMachineRunner.enabled) _stateMachineRunner.enabled = true;
        else if (_dangerLevel == 0) _stateMachineRunner.enabled = false;

        _anim.SetFloat("Speed", Mathf.Abs(_agent.velocity.magnitude));
    }

    public void Flashed(bool value) => _flashed = value;
    public void Shived(bool value) => _shived = value;
    public void HearSound(bool value) => _hearSound = value;
    public void Attack() => OnAttack?.Invoke(this, null);
    public void LostChase() => OnLostChase?.Invoke(this, null);
    
    public void SetDangerLevel(int value) => _dangerLevel = value;
}
