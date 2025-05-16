using UnityEngine;

namespace Sound
{
    public class SoundPause : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audio;
        [SerializeField] private PlayAudio shiv;
        [SerializeField] private PlayAudio death;

        public void Pause()
        {
            print("a");
            foreach (var a in audio)
            {
                print("b");
                a.volume = 0f;
            }
        }

        public void Resume()
        {
            foreach (var a in audio)
            {
                a.volume = 1f;
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