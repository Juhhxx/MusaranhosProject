using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public class Corpse : InteractiveObject
    {
        [SerializeField] private Letter letterReward;
        public UnityEvent OnInteract;

        public override void Interact()
        {
            var temp = FindFirstObjectByType<PlayerInventory>();
            if(temp != null) temp.AddLetter(letterReward);
            OnInteract.Invoke();
            base.Interact();
        }
    }
}