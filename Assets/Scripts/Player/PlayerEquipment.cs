using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private EquipmentObject[] equipmentObjects;
    private EquipmentObject CurrentEquipment => equipmentObjects[(int) _currentEquipmentEnum];
    private EquipmentEnum _currentEquipmentEnum;

    public void StoreEquipment()
    {
        CurrentEquipment.Unequip();
        _currentEquipmentEnum = EquipmentEnum.None;
    }

    public void UseEquipment(EquipmentEnum equipmentEnum)
    {
        if (_currentEquipmentEnum == equipmentEnum)
        {
            CurrentEquipment.Use();
        }
        
        StoreEquipment();
        _currentEquipmentEnum = equipmentEnum;
        CurrentEquipment.Equip();
    }
}
