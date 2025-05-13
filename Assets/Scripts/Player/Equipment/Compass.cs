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
        private float _lastInterferenceTime;
        
        [Header("Needle Options")]
        [SerializeField] private GameObject needleObject;
        [SerializeField] private float rotationSpeed = 32;
        private Animator _animator;
        
        private bool _unequipped = true;
        private GameObject _exitRoom;
        private Random _rnd;
        
        public override void Start()
        {
            _animator = GetComponent<Animator>();
            _exitRoom = GameObject.FindWithTag("ExitRoom");
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
            var rotation = Quaternion.LookRotation(transform.position - _exitRoom.transform.position, Vector3.up);
            if (IsInterferenceReady())
            {
                rotation = ApplyInterference(rotation);
                _lastInterferenceTime = 0f;
            }
            return rotation;
        }

        private bool IsInterferenceReady()
        {
            return (_lastInterferenceTime += Time.deltaTime) >= interferenceUpdateFrequency;
        }

        private Quaternion ApplyInterference(Quaternion rotation)
        {
            var interferenceModifier = GetHighestInterference();
            return rotation * Quaternion.AngleAxis((float)_rnd.NextDouble() * Mathf.Lerp(0, 360, interferenceModifier), Vector3.up);
        }

        private void RotateNeedle(Quaternion rotation)
        {
            needleObject.transform.rotation = Quaternion.RotateTowards(needleObject.transform.rotation, rotation, rotationSpeed);
        }

        private float GetHighestInterference()
        {
            float highestInterference = 0;
            var detectedInterferences = Detector.GetObjectsInArea<Interference>(transform, interferenceRange, interferenceLayerMask);
            foreach (var interference in detectedInterferences)
            {
                float temp;
                if((temp = Mathf.Lerp(0, interference.InterferenceIntensity, DistanceToTarget(transform.position, interference.transform.position))) > highestInterference)
                    highestInterference = temp;
            }
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
            _unequipped = false;
        }

        public override void Unequip()
        {
            _animator.SetTrigger("Unequip");
            _unequipped = true;
        }
    }
}