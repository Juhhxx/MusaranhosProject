using System;
using UnityEngine;

namespace Map
{
    
    [RequireComponent(typeof(BoxCollider))]
    public class InteractiveObject : MonoBehaviour
    {
        [Header("Interaction Options")]
        [SerializeField] private Item requiredItem;
        [SerializeField] private Item rewardItem;

        public Item RequiredItem => requiredItem;
        public Item RewardItem => rewardItem;

        public virtual void Interact()
        {
            enabled = false;
        }

        public void SetRequiredItem(Item item)
        {
            if(requiredItem == Item.None) requiredItem = item;
        }
    }
}