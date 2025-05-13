using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 _moveVector;
    
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        _moveVector = _playerInput.actions["Move"].ReadValue<Vector2>(); //Probably change to calling a function in PlayerMovement class
        if (_playerInput.actions["Interact"].ReadValue<int>() == 1) ; //Call player interaction
        if (_playerInput.actions["Compass"].ReadValue<int>() == 1) ; //Call player idk, equipment???
        if (_playerInput.actions["Lantern"].ReadValue<int>() == 1) ; //Call player idk, equipment???
    }
}
