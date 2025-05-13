using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float _vel;
    private PlayerInput _playerInput;
    private PlayerInteraction _playerInteraction;
    private PlayerMovement _playerMovement;
    private PlayerEquipment _playerEquipment;
    
    private Vector2 _moveVector;
    private CharacterController _playerController;
    
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInteraction = GetComponent<PlayerInteraction>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerEquipment = GetComponent<PlayerEquipment>();

        _playerController = GetComponent<CharacterController>();
        
        _playerInput.actions["Compass"].started += HoldCompass;
        _playerInput.actions["Compass"].canceled += StoreCompass;
        _playerInput.actions["Lantern"].performed += ChargeLantern;
        _playerInput.actions["Interact"].performed += Interact;
    }

    void Update()
    {
        _moveVector = _playerInput.actions["Move"].ReadValue<Vector2>(); //Probably change to calling a function in PlayerMovement class
        _playerMovement.NoGridMov(_moveVector,_vel,_playerController);
    }

    private void HoldCompass(InputAction.CallbackContext context)
    {
        _playerEquipment.UseEquipment(EquipmentEnum.Compass);
    }

    private void StoreCompass(InputAction.CallbackContext context)
    {
        _playerEquipment.StoreEquipment();
    }

    private void ChargeLantern(InputAction.CallbackContext context)
    {
        _playerEquipment.UseEquipment(EquipmentEnum.Lantern);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _playerInteraction.Interact();
    }
}
