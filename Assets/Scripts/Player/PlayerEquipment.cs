using System;
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
        if(_currentEquipmentEnum == EquipmentEnum.None) return;
        CurrentEquipment.Unequip(); 
        _currentEquipmentEnum = EquipmentEnum.None;
    }

    public void UseEquipment(EquipmentEnum equipmentEnum)
    {
        if (_currentEquipmentEnum == equipmentEnum)
        {
            CurrentEquipment.Use();
            return;
        }
        
        if(_currentEquipmentEnum != EquipmentEnum.None) StoreEquipment();
        _currentEquipmentEnum = equipmentEnum;
        CurrentEquipment.OnUnequip += OnUnequip;
        CurrentEquipment.Equip();
    }

    private void OnUnequip(object sender, EventArgs e)
    {
        _currentEquipmentEnum = EquipmentEnum.None;
    }
}
