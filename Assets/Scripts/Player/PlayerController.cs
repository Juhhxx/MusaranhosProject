using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerInput _playerInput;
    private PlayerInteraction _playerInteraction;
    private PlayerMovement _playerMovement;
    private PlayerEquipment _playerEquipment;
    private PlayerLetterReader _playerLetterReader;
    
    private Vector2 _moveVector;
    private bool _canMove = true;
    private bool _canDoAction = true;
    private bool _cantRead;
    private bool _prePauseCanMove;
    private bool _prePauseCanDoAction;
    private bool _prePauseCantRead;
    private bool _escapedFirstAttack;

    public bool EscapedFirstAttack => _escapedFirstAttack;
    public event EventHandler OnPause;
    
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInteraction = GetComponent<PlayerInteraction>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerEquipment = GetComponent<PlayerEquipment>();
        _playerLetterReader = GetComponent<PlayerLetterReader>();
        
        _playerInput.actions["Compass"].started += HoldCompass;
        _playerInput.actions["Compass"].canceled += StoreCompass;
        _playerInput.actions["Lantern"].performed += ChargeLantern;
        _playerInput.actions["Interact"].started += Interact;
        _playerInput.actions["Letters"].started += ToggleLetters;
        _playerInput.actions["NextLetter"].started += NextLetter;
        _playerInput.actions["PreviousLetter"].started += PreviousLetter;
        _playerInput.actions["Interact"].performed += Interact;
    }

    void Update()
    {
        if(_canMove) _playerMovement.MoveVector = _playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void HoldCompass(InputAction.CallbackContext context)
    {
        if(_canDoAction)_playerEquipment.UseEquipment(EquipmentEnum.Compass);
    }

    private void StoreCompass(InputAction.CallbackContext context)
    {
        if(_canDoAction)_playerEquipment.StoreEquipment();
    }

    private void ChargeLantern(InputAction.CallbackContext context)
    {
        if(_canDoAction)_playerEquipment.UseEquipment(EquipmentEnum.Lantern);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(_canDoAction)_playerInteraction.Interact();
    }

    private void ToggleLetters(InputAction.CallbackContext context)
    {
        if(_cantRead) return;
        _playerLetterReader.ToggleLetters();
    }

    private void NextLetter(InputAction.CallbackContext context)
    {
        if(_cantRead) return;
        _playerLetterReader.NextLetter();
    }

    private void PreviousLetter(InputAction.CallbackContext context)
    {
        if(_cantRead) return;
        _playerLetterReader.PreviousLetter();
    }

    public void StopActions(bool value, bool cantRead)
    {
        _canDoAction = !value;
        _cantRead = cantRead;
        _playerEquipment.StoreEquipment();
    }

    public void StopMovement(bool value)
    {
        _canMove = !value;
    }

    public void PauseGame()
    {
        _prePauseCanMove = _canMove;
        _prePauseCanDoAction = _canDoAction;
        _prePauseCantRead = _cantRead;
        StopActions(true, true);
        StopMovement(true);
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    public void ResumeGame()
    {
        StopMovement(_prePauseCanMove);
        StopActions(_prePauseCanDoAction, _prePauseCantRead);
    }

    public void GetAttacked()
    {
        StopMovement(true);
        StopActions(true, true);
    }

    public void EscapedAttack()
    {
        print("escapedAttack");
        StopMovement(false);
        StopActions(false, false);
        if (!_escapedFirstAttack) _escapedFirstAttack = true;
    }
}
