using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Map
{
    public class Dynamite : InteractiveObject
    {
        [SerializeField] private PlayAudio litPlayAudio;
        [SerializeField] private PlayAudio placedPlayAudio;
        [SerializeField] private PlayAudio explosionPlayAudio;
        
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Enable()
        {
            animator.SetTrigger("Placed");
            GetComponent<Collider>().enabled = true;
        }

        public override void Interact()
        {
            animator.SetTrigger("Lit");
        }

        public void Explosion()
        {
            animator.SetTrigger("Explosion");
        }

        public void PlayPlacedAudio()
        {
            placedPlayAudio.Play();
        }

        public void PlayLitAudio()
        {
            litPlayAudio.Play();
        }

        public void PlayExplosionAudio()
        {
            explosionPlayAudio.Play();
        }
    }
}