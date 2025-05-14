using UnityEngine;

namespace Misc
{
    public class SoundClue : MonoBehaviour
    {
        public void Start()
        {
            var position = transform.position;
            FindFirstObjectByType<GameManager>().NewSound(position);
            Destroy(this);
        }
        
    }
}