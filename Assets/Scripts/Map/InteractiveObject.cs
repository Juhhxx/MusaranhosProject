using System;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    
    [RequireComponent(typeof(BoxCollider))]
    public class InteractiveObject : MonoBehaviour
    {
        [Header("Interaction Options")]
        [SerializeField] protected Item requiredItem;
        [SerializeField] private Item[] rewardItem;
        public UnityEvent OnInteract;
        
        public Item RequiredItem => requiredItem;
        public Item[] RewardItem => rewardItem;

        public virtual void Interact()
        {
            var temp = FindFirstObjectByType<PlayerInventory>();
            foreach (Item item in rewardItem)
            {
                if(item != Item.None) temp.AddItem(item);
            }
            OnInteract.Invoke();
            enabled = false;
        }

        public void SetRequiredItem(Item item)
        {
            if(requiredItem == Item.None) requiredItem = item;
        }
    }
}