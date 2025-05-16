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

        public override void Interact()
        {
            var temp = FindFirstObjectByType<PlayerInventory>();
            if(temp != null) temp.AddLetter(letterReward);
            Disable();
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