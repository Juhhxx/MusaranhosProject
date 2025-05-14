using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _gridSize;
    [SerializeField] private LayerMask _layerBlock;
    [SerializeField] private float _velocity;
    
    private Vector3             _mov;
    private Vector3             _rotation;
    private bool                cantGo;

    private CharacterController _playerController;

    void Start()
    {
        _rotation = new Vector3(0,90,0);
        _playerController = GetComponent<CharacterController>();
    }

    void Update()
    {   
        cantGo = Physics.Raycast(transform.position,transform.forward, _gridSize, _layerBlock);

        if (Input.GetKeyDown(KeyCode.A)) { transform.Rotate(_rotation);  }
        if (Input.GetKeyDown(KeyCode.D)) { transform.Rotate(-_rotation); }
        if (Input.GetKeyDown(KeyCode.W) && !cantGo) {_playerController.Move(transform.forward*_velocity);  }
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
