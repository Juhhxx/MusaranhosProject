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
        private IntervaledSoundPlayer[] dangerVoices;
        private EnemyController eController;
        private int dangerLevel;
        private IntervaledSoundPlayer currentSoundPlayer;

        private void Start()
        {
            eController = GetComponent<EnemyController>();
            dangerVoices = new IntervaledSoundPlayer[2];
            dangerVoices[0] = danger1Voice;
            dangerVoices[1] = danger2Voice;
        }

        private void Update()
        {
            if (dangerLevel != eController.DangerLevel)
            {
                currentSoundPlayer.StopPlaying();
                dangerLevel = eController.DangerLevel;
                if(dangerVoices.Length >= dangerLevel) currentSoundPlayer = dangerVoices[dangerLevel-1];
            }
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