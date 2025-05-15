using System;
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
            enabled = false;
            OnInteract.Invoke();
        }

        public void SetRequiredItem(Item item)
        {
            if(requiredItem == Item.None) requiredItem = item;
        }
    }
}