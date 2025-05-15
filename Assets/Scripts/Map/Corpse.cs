using Player;
using UnityEngine;

namespace Map
{
    public class Corpse : InteractiveObject
    {
        [SerializeField] private Letter letterReward;

        public override void Interact()
        {
            var temp = FindFirstObjectByType<PlayerInventory>();
            if(temp != null) temp.AddLetter(letterReward);
            base.Interact();
        }
    }
}