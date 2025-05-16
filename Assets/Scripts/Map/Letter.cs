using UnityEngine;

namespace Map
{
    public class Letter : MonoBehaviour
    {
        [SerializeField] private Sprite letterImage;
        public Sprite Image => letterImage;
    }
}