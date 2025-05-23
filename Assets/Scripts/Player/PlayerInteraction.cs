using System;
using Map;
using Player;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1f;
    [SerializeField] private Transform interactionOffset;
    [SerializeField] private LayerMask interactiveLayerMask;
    
    private bool _canInteract = true;
    
    private PlayerInventory playerInventory;
    
    public event EventHandler OnInteract;

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
        var detectedObject = Detector.GetClosestInArea<InteractiveObject>(interactionOffset, interactionRadius, interactiveLayerMask);
        print(detectedObject);
        if (CanInteract(detectedObject))
        {
            foreach (var item in detectedObject.RewardItem)
            {
                if(item != Item.None && item != Item.Block) playerInventory.AddItem(item);
            }
            detectedObject.Interact();   
            OnInteract?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool CanInteract(InteractiveObject interactiveObject)
    {
        if(!_canInteract) return false;
        if (playerInventory != null && interactiveObject != null && interactiveObject.isActiveAndEnabled)
        {
            return playerInventory.ContainsItem(interactiveObject.RequiredItem);
        }
        return false;
    }
}
