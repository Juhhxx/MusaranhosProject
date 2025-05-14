using System;
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
        [SerializeField] private float unequippedLossMultiplier;

        public float LightLevel
        {
            get => _lightLevel;
            private set
            {
                _lightLevel = Mathf.Clamp(value, 0f, maxLightLevel); 
                if(_light != null) _light.intensity = Mathf.Lerp(0f, maxLightIntensity, _lightLevel/maxLightLevel);
            }
        }
        
        private float _lightLevel;
        
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
            if (LightLevel > 0) DimLight();
        }

        private void DimLight()
        {
            var loss = lanternLossPerSecond * Time.deltaTime;
            if(_unequipped) loss *= unequippedLossMultiplier;
            LightLevel -= loss;
        }

        public override void Use()
        {
            LightLevel += lanternGainPerUse;
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
        }
    }
}