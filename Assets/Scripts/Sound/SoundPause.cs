using UnityEngine;

namespace Sound
{
    public class SoundPause : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audio;

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
    }
}