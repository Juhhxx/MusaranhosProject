using UnityEngine;

namespace Misc
{
    public class SoundClue : MonoBehaviour
    {
        public void SetPosition(Vector3 pos)
        {
            var position = transform.position;
            FindFirstObjectByType<GameManager>().NewSound(position);
            Destroy(this);
        }
        
    }
}