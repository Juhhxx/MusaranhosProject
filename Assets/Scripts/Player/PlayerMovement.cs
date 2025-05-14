using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("WalkField")]
    [SerializeField] private int _gridSizePlusHalf;
    [SerializeField] private LayerMask _layerBlock;
    [SerializeField] private float _camRotateSpeed = 1;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _velocityOffGrid;
    
    private Vector3             _mov;
    private Vector3            _rotationPerTurn;
    private Vector3            _rotation;
    //Conditions
    private bool                _cantGo;
    [SerializeField] private bool _stalker; //Precisarei de um GetBool no futuro

    private CharacterController _playerController;

    [Header("Camera Rotation No Grid")]
    [SerializeField] private float _sensitivity;
    private PlayerInput _playerInput;
    private Vector2 _mouseDir;

    [Header("Back to the Grid")]
    [SerializeField] private bool _goBackToGrid;
    //[SerializeField] private LayerMask _backToGridMask;
    private Vector3 _lastPos;

    void Start()
    {
       _rotationPerTurn = new Vector3(0,90,0);
       _rotation = Vector3.zero;
        
        _playerInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<CharacterController>();
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
            if (_lastPos != Vector3.zero){ResetGrid();}
            else {OffGridMov();}

            if (transform.position == _lastPos) {_goBackToGrid = false;}
        }
        else if (!_stalker && !_goBackToGrid) 
        {
            Debug.Log("doing grid movements");
            RotateVector();
            Rotate();
            GridMov();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EditorOnly")
        {
            _lastPos = other.transform.position;
            _lastPos.y = transform.position.y;
            Debug.Log(_lastPos);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "EditorOnly")
        {
            _lastPos = Vector3.zero; 
            Debug.Log(_lastPos);
        }
    }

    void OffGridMov()
    {
        float rotation = _mouseDir.x * _sensitivity;
        transform.Rotate(0f,rotation,0f);

        if (Input.GetKey(KeyCode.W)) 
        {_playerController.Move(transform.forward*_velocityOffGrid*Time.fixedDeltaTime);}
    }

    void GridMov()
    {
        if (Input.GetKeyDown(KeyCode.W) && !_cantGo)
            {_playerController.Move(transform.forward*_velocity.z);}
    }


    void ResetGrid()
    {
        transform.position = Vector3.MoveTowards(transform.position,_lastPos,_velocity.magnitude*Time.deltaTime);
        transform.rotation = Quaternion.identity;
    }

    private void RotateVector()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        { 
            _rotation -= _rotationPerTurn;//set vector3 rotation to -90 at Y Axis 
        }
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            _rotation += _rotationPerTurn;//set vector3 rotation to 90 at Y Axis 
        }
    }

    private void Rotate()
    {
        Quaternion _rotQua = Quaternion.Euler(_rotation);
        transform.localRotation = Quaternion.Slerp(transform.rotation,_rotQua,_camRotateSpeed*Time.deltaTime);
        Debug.Log(_rotation);
    }
}
