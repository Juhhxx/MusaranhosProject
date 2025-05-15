using System;
using Map;
using UnityEngine;
using Random = System.Random;

namespace Player.Equipment
{
    public class Compass : EquipmentObject
    {
        [Header("Interference Options ")]
        [SerializeField] private float interferenceRange = 2f;
        [SerializeField] private LayerMask interferenceLayerMask;
        [SerializeField] private float interferenceUpdateFrequency = 0.5f;
        [SerializeField] private float interferenceRotationModifier;
        [SerializeField] private float interferenceDuration;
        private float _lastInterferenceTime;
        private float _interferenceTimer;
        private Quaternion _lastInterferenceRotation;
        private float _lastInterferenceRotationModifier;
        
        [Header("Needle Options")]
        [SerializeField] private float rotationSpeed = 32;
        private float _rotationModifier;
        
        [Header("Model Options")]
        [SerializeField] private GameObject modelObject;
        [SerializeField] private Transform needleObject;
        [SerializeField] private Transform needleRotationObject;
        private Animator _animator;
        
        
        private bool _unequipped = true;
        private Transform _needleTarget;
        private Random _rnd;
        
        public override void Start()
        {
            _animator = GetComponent<Animator>();
            _needleTarget = GameObject.FindWithTag("Exit").transform;
            _rnd = new Random();
        }

        private void Update()
        {
            if (!_unequipped)
            {
                RotateNeedle(GetNeedleRotation());
            }
        }

        private Quaternion GetNeedleRotation()
        {
            var rotation = Quaternion.LookRotation(_needleTarget.transform.position - transform.position, Vector3.up);
            if (IsInterferenceReady())
            {
                _lastInterferenceTime = 0f;
                return ApplyInterference(rotation);
            }

            if (_lastInterferenceTime < interferenceDuration)
            {
                return rotation * _lastInterferenceRotation;
            }
            _rotationModifier = 1;
            return rotation;
        }

        private bool IsInterferenceReady()
        {
            return (_lastInterferenceTime += Time.deltaTime) >= interferenceUpdateFrequency;
        }

        private Quaternion ApplyInterference(Quaternion rotation)
        {
            var interferenceModifier = GetHighestInterference();
             _lastInterferenceRotation =
                Quaternion.AngleAxis((float)_rnd.NextDouble() * Mathf.Lerp(0, 360, interferenceModifier), Vector3.up);
            return rotation * _lastInterferenceRotation;
        }

        private void RotateNeedle(Quaternion rotation)
        {
            var rawRotation = Quaternion.RotateTowards(needleRotationObject.transform.rotation, rotation,
                rotationSpeed * _rotationModifier * Time.deltaTime);
            needleRotationObject.rotation = rawRotation; 
            needleObject.eulerAngles = new Vector3(needleObject.eulerAngles.x, needleObject.eulerAngles.y, -needleRotationObject.localEulerAngles.y);
        }

        private float GetHighestInterference()
        {
            float highestInterference = 0;
            float distance = interferenceRange * 2;
            var detectedInterferences = Detector.GetObjectsInArea<Interference>(transform, interferenceRange, interferenceLayerMask);
            
            foreach (var interference in detectedInterferences)
            {
                distance = DistanceToTarget(transform.position, interference.transform.position);
                float temp;
                if((temp = Mathf.Lerp(0, interference.InterferenceIntensity, (interferenceRange * 2 - distance) / interferenceRange * 2)) > highestInterference)
                    highestInterference = temp;
            }
            
            _rotationModifier *= _lastInterferenceRotationModifier = Mathf.Lerp(0, interferenceRotationModifier,(interferenceRange * 2 - distance) / (interferenceRange * 2));
            return highestInterference;
        }

        private float DistanceToTarget(Vector3 startPosition, Vector3 targetPosition)
        {
            return Vector3.Distance(startPosition, targetPosition);
        }

        public override void Use()
        {
            
        }

        public override void Equip()
        {
            _animator.SetTrigger("Equip");
            modelObject.SetActive(true);
            _unequipped = false;
        }

        public override void Unequip()
        {
            _animator.SetTrigger("Unequip");
            modelObject.SetActive(false);
            _unequipped = true;
        }

        public void SetNeedleTarget(Transform needleTarget)
        {
            _needleTarget = needleTarget;
        }
    }
}