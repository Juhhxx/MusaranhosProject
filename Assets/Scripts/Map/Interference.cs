using UnityEngine;

namespace Map
{
    public class Interference : MonoBehaviour
    {
        [Header("Interference Options")]
        [SerializeField] private float interferenceIntensity = 1;

        public float InterferenceIntensity
        {
            get => interferenceIntensity;
            private set => interferenceIntensity = value;
        }
    }
}