using System;
using UnityEngine;

namespace Player
{
    public abstract class EquipmentObject : MonoBehaviour
    {
        
        [Header("Audio Options")]
        [SerializeField] protected PlayAudio pullUpAudio;
        [SerializeField] protected PlayAudio pullDownAudio;
        [SerializeField] protected PlayAudio useAudio;
        
        public event EventHandler OnUnequip;
        
        public abstract void Start();

        public abstract void Use();
        
        public abstract void Equip();
        
        public abstract void Unequip();
        
        public virtual void PlayPullUpAudio()
        {
            if(pullUpAudio != null) pullUpAudio.Play();
        }

        public virtual void PlayPullDownAudio()
        {
            if(pullDownAudio != null) pullDownAudio.Play();
        }
        public virtual void PlayUseAudio()
        {
            if(useAudio != null) useAudio.Play();
        }

        protected void InvokeOnUnequip()
        {
            OnUnequip?.Invoke(this, EventArgs.Empty);
        }
    }
}