using UnityEngine;

namespace Sound
{
    public class SoundPause : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audio;
        [SerializeField] private AudioSource shiv;
        [SerializeField] private AudioSource death;

        public void Pause()
        {
            foreach (var a in audio)
            {
                a.Pause();
            }
        }

        public void Resume()
        {
            foreach (var a in audio)
            {
                a.UnPause();
            }
        }

        public void PlayDeath()
        {
            death.Play();
        }

        public void PlayShiv()
        {
            shiv.Play();
        }
    }
}