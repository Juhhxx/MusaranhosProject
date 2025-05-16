using System;
using Player;
using Sound;
using UnityEngine;

namespace Map
{
    public class Corpse : InteractiveObject
    {
        [SerializeField] private Letter letterReward;
        [SerializeField] private Item realRequiredItem;
        private IntervaledSoundPlayer voicePlayer;
        
        private void Start()
        {
            voicePlayer = GetComponent<IntervaledSoundPlayer>();
        }

        public override void Interact()
        {
            var temp = FindFirstObjectByType<PlayerInventory>();
            if(temp != null && letterReward != null) temp.AddLetter(letterReward);
            base.Interact();
        }

        public void Enable()
        {
            requiredItem = realRequiredItem;
            voicePlayer.StartPlaying();
        }

        private void Disable()
        {
            voicePlayer.StopPlaying();
        }
    }
}