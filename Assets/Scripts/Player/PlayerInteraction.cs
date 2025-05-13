using Map;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactiveLayerMask;
    
    private bool _canInteract;

    public void ToggleInteraction(bool? newState = null)
    {
        if(newState is null) _canInteract = !_canInteract;
        else _canInteract = newState.Value;
    }
    
    public void Interact()
    {
        if(!_canInteract) return;
        var detectedObject = Detector.GetClosestInArea<InteractiveObject>(transform, interactionRange, interactiveLayerMask);
        //Handle interactive specific behaviour;
    }
}
