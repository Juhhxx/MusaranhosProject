using System;
using System.Threading;
using Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("WalkField")]
    [SerializeField] private int _gridSizePlusHalf;
    [SerializeField] private float _walkDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private LayerMask _layerBlock;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _velocityOffGrid;
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

    [Header("Camera Rotation No Grid")]
    [SerializeField] private float _sensitivity;
    private PlayerInput _playerInput;
    private Vector2 _mouseDir;

    [Header("Back to the Grid")]
    [SerializeField] private bool _goBackToGrid;
    private Vector3 _triggerPos;

    public Vector2 MoveVector { get; set; }

    public event EventHandler OnScoutMove;

    void Start()
    {  
        _playerInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<CharacterController>();
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {   
        _cantGo = Physics.Raycast(transform.position,transform.forward, _gridSizePlusHalf, _layerBlock);
        _mouseDir = _playerInput.actions["Look"].ReadValue<Vector2>();
        
        if (_stalker)
        {
            _goBackToGrid = true;
            OffGridMov();
        }
        else if(!_stalker && _goBackToGrid)
        {
            if (_triggerPos != Vector3.zero)
            {
                transform.position = Vector3.MoveTowards(transform.position,_triggerPos,_velocity.magnitude*Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }
            else {OffGridMov();}

            if (transform.position == _triggerPos) {_goBackToGrid = false;}
        }
        else if (!_stalker && !_goBackToGrid) {GridMov();}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EditorOnly")
        {
            _triggerPos = other.transform.position;
            _triggerPos.y = transform.position.y;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "EditorOnly")
        {
            _triggerPos = Vector3.zero; 
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

    private void Moving()
    {
        _walkTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(_lastPosition,_moveTarget,_walkTimer / _walkDuration);
        if (_walkTimer >= _walkDuration)
        {
            _isMoving = false;
            _walkTimer = 0;
            _lastPosition = Vector3.zero;
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

    public void StartChase()
    {
        _stalker = true;
    }

    public void StartScout()
    {
        _stalker = false;
    }
}
