using System;
using Sound;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Enemy
{
    public class VoiceMimicry : MonoBehaviour
    {
        [SerializeField] private IntervaledSoundPlayer danger1Voice;
        [SerializeField] private IntervaledSoundPlayer danger2Voice;
        private EnemyController eController;
        private int dangerLevel;
        private IntervaledSoundPlayer currentSoundPlayer;

        private void Start()
        {
            eController = GetComponent<EnemyController>();
        }

        private void Update()
        {
            
        }

        public void Stop()
        {
            if(currentSoundPlayer != null) currentSoundPlayer.StopPlaying();
        }

        public void StartPlaying()
        {
            if(currentSoundPlayer != null) currentSoundPlayer.StartPlaying();
        }
    }
}