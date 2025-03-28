using UnityEngine;

public interface IEquipmentComponent
{
    void Equip(Item item);
    
    void Unequip(Item item);
}
