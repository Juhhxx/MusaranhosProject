using System;
using Map;
using Misc;
using Player;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class PlayerMovement : MonoBehaviour
{
    [Header("WalkField")]
    [SerializeField] private int _gridSizePlusHalf;
    [SerializeField] private float _walkDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private LayerMask _layerBlock;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _velocityOffGrid;
    [SerializeField] private Animator _animator;
    
    [Header("Noise Making Settings")]
    [SerializeField] private float _minNoiseFrequency;
    [SerializeField] private float _maxNoiseFrequency;
    private float _noiseFrequency;
    private float _noiseTimer;
    
    private float             _walkTimer;
    private float             _rotateTimer;
    private bool _isMoving;
    private bool _isRotating;
    private Vector3 _lastPosition;
    private Vector3 _moveTarget;
    private Quaternion _lastRotation;
    private Quaternion _rotateTarget;
    private GameManager _gameManager;

    //Conditions
    private bool                _cantGo;
    [SerializeField] private bool _stalker; 
    private CharacterController _playerController;
    private PlayerInventory _playerInventory;

    [Header("Camera Rotation No Grid")]
    [SerializeField] private float _sensitivity;
    private PlayerInput _playerInput;
    private Vector2 _mouseDir;

    public Vector2 MoveVector { get; set; }
    private Random _rnd;
    public event EventHandler OnScoutMove;
    public event EventHandler OnNoise;
    public event EventHandler OnShiv;
    public event EventHandler OnDeath;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<CharacterController>();
        _gameManager = FindFirstObjectByType<GameManager>();
        _playerInventory = GetComponent<PlayerInventory>();
        _rnd = new Random();
        GetComponent<Collider>().enabled = true;
        FindFirstObjectByType<EnemyController>().OnLostChase += ResetToGrid;
    }

    void Update()
    {   
        _cantGo = Physics.Raycast(transform.position,transform.forward, _gridSizePlusHalf, _layerBlock);
        _mouseDir = _playerInput.actions["Look"].ReadValue<Vector2>();

        if (_stalker)
        {
            OffGridMov();
        }
        else
        {
            GridMov();
        }
    }

    void OffGridMov()
    {
        float rotation = _mouseDir.x * _sensitivity;
        transform.Rotate(0f,rotation,0f);

        if (MoveVector.y > 0) 
        {_playerController.Move(transform.forward * (_velocityOffGrid * Time.fixedDeltaTime));}
    }

    void GridMov()
    {
        if (_isMoving) Moving();
        else if (_isRotating) Rotating();
        else GetMovingOrRotating();
    }

    public void ResetToGrid(object sender, EventArgs args)
    {
        Transform point = Detector.GetClosestInArea<GraphPoint>(transform, 15f, LayerMask.NameToLayer("GraphPoint")).transform;

        transform.position = point.position;
        transform.rotation = quaternion.identity;
    }

    private void Moving()
    {
        _walkTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(_lastPosition, _moveTarget, _walkTimer / _walkDuration);
        NoiseUpdate();
        if (_walkTimer >= _walkDuration)
        {
            _isMoving = false;
            _walkTimer = 0;
            _lastPosition = Vector3.zero;
            _animator.SetBool("Crawling", false);
            OnScoutMove?.Invoke(this, EventArgs.Empty);
            _gameManager.PlayerWalking(true);
        }
    }

    private void Rotating()
    {
        _rotateTimer += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(_lastRotation,_rotateTarget,_rotateTimer / _rotateDuration);
        if (_rotateTimer >= _rotateDuration)
        {
            _isRotating = false;
            _rotateTimer = 0;
        }
    }

    private void GetMovingOrRotating()
    {
        if (MoveVector.y > 0 && !_cantGo)
        {
            _isMoving = true;
            _animator.SetBool("Crawling", true);
            _lastPosition = transform.position;
            _moveTarget = transform.position + transform.forward * _gridSizePlusHalf;
            _gameManager.PlayerWalking(false);
            return;
        }

        if (MoveVector.x != 0)
        {
            _isRotating = true;
            _lastRotation = transform.rotation;
            _rotateTarget = transform.rotation * Quaternion.AngleAxis(MoveVector.x > 0 ? 90 : -90, Vector3.up);
        }
    }

    private void NoiseUpdate()
    {
        if (_noiseTimer == 0) GetNewNoiseFrequency();
        _noiseTimer += Time.deltaTime;
        if (_noiseTimer >= _noiseFrequency)
        {
            _noiseTimer = 0;
            //Call PlayAudio with soundclue
            OnNoise?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GetNewNoiseFrequency()
    {
        _noiseFrequency = Mathf.Lerp(_minNoiseFrequency, _maxNoiseFrequency, (float)_rnd.NextDouble());
    }

    public void StartChase()
    {
        _stalker = true;
    }

    public void StartScout()
    {
        _stalker = false;
    }

    public void SetSensitivity(float value)
    {
         _sensitivity = value;
    }

    public void GetAttacked()
    {
        if (_playerInventory.ContainsItem(Item.Shiv))
        {
            _playerInventory.RemoveItem(Item.Shiv);
            OnShiv?.Invoke(this, EventArgs.Empty);
            return;
        }
        OnDeath?.Invoke(this, EventArgs.Empty);
    }
}
