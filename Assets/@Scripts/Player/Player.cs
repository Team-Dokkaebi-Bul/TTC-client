using Eu4ng.Utilities;
using UnityEngine;
using Weapon;

/// <summary>
/// 플레이어 캐릭터 컨테이너 클래스
/// </summary>
[RequireComponent(typeof(StatComponent), typeof(WeaponSlot))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField, ReadOnly] PlayerData m_PlayerData;
    
    /* Properties */

    public IInventoryComponent InventoryInterface => null;
    public IEquipmentComponent EquipmentInterface => null;
    public IStatComponent StatInterface => null;
    
    public virtual void Initialize(PlayerData NewPlayerData)
    {
        m_PlayerData = NewPlayerData;
        
        // TODO 스탯 및 버프 설정
        
        // TODO 사용 가능한 스킬 설정
        
        // TODO 무기 장착
    }
    
    /* IPlayer */

    public void HasItem(Item item) => InventoryInterface.HasItem(item);

    public void AddItem(Item item) => InventoryInterface.AddItem(item);

    public void RemoveItem(Item item) => InventoryInterface.RemoveItem(item);

    public void Equip(Item item)
    {
        if (item.IsNotValid) return;
        
        RemoveItem(item);
        EquipmentInterface.Equip(item);
        AddStat(item.Definition.GetStat());
    }

    public void Unequip(Item item)
    {
        if (item.IsNotValid) return;
        
        AddItem(item);
        EquipmentInterface.Unequip(item);
        RemoveStat(item.Definition.GetStat());
    }

    public void AddStat(IStat stat) => StatInterface.AddStat(stat);

    public void RemoveStat(IStat stat) => StatInterface.RemoveStat(stat);
}
