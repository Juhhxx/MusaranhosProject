using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 _moveVector;
    
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Compass"].started += HoldCompass;
        _playerInput.actions["Compass"].canceled += StoreCompass;
        _playerInput.actions["Lantern"].performed += ChargeLantern;
        _playerInput.actions["Lantern"].performed += Interact;
    }

    // Update is called once per frame
    void Update()
    {
        _moveVector = _playerInput.actions["Move"].ReadValue<Vector2>(); //Probably change to calling a function in PlayerMovement class
    }

    private void HoldCompass(InputAction.CallbackContext context)
    {
        //Call player idk, equipment???
    }

    private void StoreCompass(InputAction.CallbackContext context)
    {
        //Call player idk, equipment???
    }

    private void ChargeLantern(InputAction.CallbackContext context)
    {
        //Call player idk, equipment???
    }

    private void Interact(InputAction.CallbackContext context)
    {
        // call playerInteraction
    }
}
