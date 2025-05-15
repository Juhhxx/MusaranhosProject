using System;
using UnityEngine;

namespace Player
{
    public abstract class EquipmentObject : MonoBehaviour
    {
        
        [Header("Audio Options")]
        [SerializeField] private PlayAudio pullUpAudio;
        [SerializeField] private PlayAudio pullDownAudio;
        [SerializeField] private PlayAudio useAudio;
        
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
    }
}