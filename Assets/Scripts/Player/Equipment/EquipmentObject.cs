using UnityEngine;

namespace Player
{
    public abstract class EquipmentObject : MonoBehaviour
    {
        public abstract void Use();
        
        public abstract void Equip();
        
        public abstract void Unequip();
    }
}