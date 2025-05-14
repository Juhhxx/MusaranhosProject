using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("WalkField")]
    [SerializeField] private int _gridSizePlusHalf;
    [SerializeField] private LayerMask _layerBlock;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _velocityOffGrid;
    
    private Vector3             _mov;
    private Vector3             _rotation;

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
        _rotation = new Vector3(0,90,0);
        
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
            if (_lastPos != Vector3.zero)
            {
                transform.position = Vector3.MoveTowards(transform.position,_lastPos,_velocity.magnitude*Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }
            else {OffGridMov();}

            if (transform.position == _lastPos) {_goBackToGrid = false;}
        }
        else if (!_stalker && !_goBackToGrid) {GridMov();}
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
        if (Input.GetKeyDown(KeyCode.A)) { transform.Rotate(_rotation);  }
        if (Input.GetKeyDown(KeyCode.D)) { transform.Rotate(-_rotation); }
        if (Input.GetKeyDown(KeyCode.W) && !_cantGo)
            {_playerController.Move(transform.forward*_velocity.z);}
    }

    // public void NoGridMov(Vector2 dir, float velocity, CharacterController _playerController, Transform _playerTrans)
    // {
    //     if (dir.magnitude > 0) _mov = _playerTrans.forward*velocity; 
    //     else _mov = Vector2.zero;

    //     _playerController.Move(_mov);
    // }

    // public void RotationMov(Vector2 dir, Transform _trans)
    // {
    //     if(dir.x > 0){_trans.Rotate(_rotation);}
    //     else if(dir.x < 0){_trans.Rotate(-_rotation);}
    // }
}
