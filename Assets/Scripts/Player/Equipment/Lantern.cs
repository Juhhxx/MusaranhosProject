using System;
using Misc;
using UnityEngine;

namespace Player.Equipment
{
    public class Lantern : EquipmentObject
    {
        [Header("Light Options")] 
        [SerializeField] private Color lightColor;
        [SerializeField] private float maxLightIntensity;
        [SerializeField] private float maxLightLevel;
        [SerializeField] private float lanternGainPerUse;
        [SerializeField] private float lanternLossPerSecond;
        [SerializeField] private LayerMask _raycastMask;
        [SerializeField] private float unequippedLossMultiplier;
        
        [Header("Sound Options")]
        [SerializeField] private float maxSoundVolume;
        [SerializeField] private float soundGain;
        [SerializeField] private float SoundLoss;
        
        [Header("Flashing Options")]
        [SerializeField] private float raycastMultiplier;

        public event EventHandler OnCrank;
        public event EventHandler OnFlash;

        public float LightLevel
        {
            get => _lightLevel;
            private set
            {
                _lightLevel = Mathf.Clamp(value, 0f, maxLightLevel); 
                _animator.SetFloat("CrankSpeed", Mathf.Clamp(LightLevel / 2, 0f, maxLightLevel / 2));
                if(_light != null) _light.intensity = Mathf.Lerp(0f, maxLightIntensity, _lightLevel/maxLightLevel);
            }
        }

        public float SoundLevel
        {
            get => _soundLevel;
            private set
            {
                _soundLevel = Mathf.Clamp(value, 0f, maxSoundVolume);
                UpdateAudioPlayers();
            }
        }
        
        private float _lightLevel;
        private float _soundLevel;
        
        private Animator _animator;
        private Light _light;
        private bool _unequipped = true; 
        
        public override void Start()
        {
            _animator = GetComponent<Animator>();
            _light = GetComponentInChildren<Light>();
            if(_light != null) _light.color = lightColor;
        }

        private void Update()
        {
            if (LightLevel > 0)
            {
                CheckFlashing();
                DimLight();
            }
            if(SoundLevel > 0) DimSound();
            if(_lightLevel == 0 && !_unequipped) Unequip();
        }

        private void CheckFlashing()
        {
            EnemyController temp;
            if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _lightLevel * raycastMultiplier, _raycastMask))
                if ((temp = hit.collider.gameObject.GetComponent<EnemyController>()) != null)
                {
                    temp.Flashed(true);
                    OnFlash?.Invoke(this, EventArgs.Empty);
                }
                    
        }

        private void DimLight()
        {
            var loss = lanternLossPerSecond * Time.deltaTime;
            if(_unequipped) loss *= unequippedLossMultiplier;
            LightLevel -= loss;
        }

        private void DimSound()
        {
            var loss = lanternLossPerSecond * SoundLoss * Time.deltaTime;
            if(_unequipped) loss *= unequippedLossMultiplier;
            SoundLevel -= loss;
        }

        public override void Use()
        {
            LightLevel += lanternGainPerUse;
            SoundLevel += lanternLossPerSecond * soundGain;
            OnCrank?.Invoke(this, EventArgs.Empty);
        }

        public override void Equip()
        {
            Use();
            _unequipped = false;
            _animator.SetTrigger("Equip");
        }

        public override void Unequip()
        {
            _unequipped = true;
            _animator.SetTrigger("Unequip");
            InvokeOnUnequip();
        }

        private void UpdateAudioPlayers()
        {
            useAudio._audioSource.pitch = _soundLevel;
            useAudio._audioSource.volume = _soundLevel;
        }
    }
}