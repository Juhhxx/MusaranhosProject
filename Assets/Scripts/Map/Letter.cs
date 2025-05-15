using UnityEngine;

namespace Map
{
    public class Letter : MonoBehaviour
    {
        [SerializeField] private string text;
        public string Text => text;
    }
}