using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private EquipmentObject[] equipmentObjects;
    private EquipmentObject CurrentEquipment => equipmentObjects[(int) _currentEquipmentEnum -1];
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
        
        if(_currentEquipmentEnum != EquipmentEnum.None) StoreEquipment();
        _currentEquipmentEnum = equipmentEnum;
        CurrentEquipment.Equip();
    }
}
