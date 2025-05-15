using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Sound
{
    public class IntervaledSoundPlayer : MonoBehaviour
    {
        [Header("Audio Settings")] 
        [SerializeField] private float minIntervalDuration;
        [SerializeField] private float maxIntervalDuration;
        [SerializeField] private bool playOnStart;
        [SerializeField] private PlayAudio playAudio;

        private bool play;
        private bool waiting;
        private Random _rnd;

        private void Start()
        {
            _rnd = new Random();
            if (playOnStart) StartPlaying();
        }

        private void Update()
        {
            if (play && !waiting)
            {
                waiting = true;
                StartCoroutine(PlayAfterInterval(GetInterval()));
            }
        }

        public void StartPlaying()
        {
            play = true;
        }

        public void StopPlaying()
        {
            play = false;
        }

        private IEnumerator PlayAfterInterval(float interval)
        {
            yield return new WaitForSeconds(interval);
            playAudio.Play();
            waiting = false;
        }

        private float GetInterval()
        {
            return Mathf.Lerp(minIntervalDuration, maxIntervalDuration, (float)_rnd.NextDouble());
        }
    }
}