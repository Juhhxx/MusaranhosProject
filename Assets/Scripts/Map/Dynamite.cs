using System;
using UnityEngine;

namespace Map
{
    public class Dynamite : InteractiveObject
    {
        private Animator animator;

        public void Start()
        {
            SetRequiredItem(Item.Lighter);
            animator = GetComponent<Animator>();
            animator.SetTrigger("Placed");
        }

        public override void Interact()
        {
            animator.SetTrigger("Lit");
        }

        public void Explosion()
        {
            
        }
    }
}