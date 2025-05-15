using System;
using UnityEngine;

namespace Map
{
    public class Dynamite : InteractiveObject
    {
        [SerializeField] private PlayAudio litPlayAudio;
        [SerializeField] private PlayAudio placedPlayAudio;
        [SerializeField] private PlayAudio explosionPlayAudio;
        
        private Animator animator;

        public void Start()
        {
            SetRequiredItem(Item.Lighter);
            animator = GetComponent<Animator>();
            animator.SetTrigger("Placed");
            placedPlayAudio.Play();
        }

        public override void Interact()
        {
            animator.SetTrigger("Lit");
            litPlayAudio.Play();
        }

        public void Explosion()
        {
            explosionPlayAudio.Play();
        }
    }
}