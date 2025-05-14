using System;
using Map;
using Player;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1f;
    [SerializeField] private Vector3 interactionOffset = Vector3.up;
    [SerializeField] private LayerMask interactiveLayerMask;
    
    private bool _canInteract;
    
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void ToggleInteraction(bool? newState = null)
    {
        if(newState is null) _canInteract = !_canInteract;
        else _canInteract = newState.Value;
    }
    
    public void Interact()
    {
        if(!_canInteract) return;
        var detectedObject = Detector.GetClosestInArea<InteractiveObject>(transform, interactionRadius, interactiveLayerMask);
        if (CanInteract(detectedObject))
        {
            if(detectedObject.RewardItem != Item.None) playerInventory.AddItem(detectedObject.RewardItem);
            detectedObject.Interact();   
        }
    }

    private bool CanInteract(InteractiveObject interactiveObject)
    {
        if(!_canInteract) return false;
        if (playerInventory != null && interactiveObject != null)
        {
            if(interactiveObject.RequiredItem != Item.None)
                playerInventory.ContainsItem(interactiveObject.RequiredItem);
        }
        return true;
    }
}
