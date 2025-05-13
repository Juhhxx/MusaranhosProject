using Player;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private EquipmentEnum _currentEquipmentEnum;

    public void StoreEquipment()
    {
        _currentEquipmentEnum = EquipmentEnum.None;
    }

    public void UseEquipment(EquipmentEnum equipmentEnum)
    {
        if (_currentEquipmentEnum == equipmentEnum)
        {
            //Call equipment specific behaviour
        }
        
        StoreEquipment();
        _currentEquipmentEnum = equipmentEnum;
    }
}
