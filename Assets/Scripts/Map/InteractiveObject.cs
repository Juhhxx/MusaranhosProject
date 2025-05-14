using UnityEngine;

namespace Map
{
    public class InteractiveObject : MonoBehaviour
    {
        [Header("Interaction Options")]
        [SerializeField] private Item requiredItem;
        [SerializeField] private Item rewardItem;
        
        public Item RequiredItem => requiredItem;
        public Item RewardItem => rewardItem;

        public void Interact()
        {
            enabled = false;
        }
    }
}