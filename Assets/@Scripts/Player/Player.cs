using Eu4ng.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Juhyeon.Weapon.System;

/// <summary>
/// 플레이어 캐릭터 컨테이너 클래스
/// </summary>
[RequireComponent(typeof(StatComponent), typeof(EquipmentComponent))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField, ReadOnly] PlayerData m_PlayerData;

    private EquipmentComponent _equipmentComponent;

    /* Properties */

    public IInventoryComponent InventoryInterface => null;
    public IEquipmentComponent EquipmentInterface => _equipmentComponent;
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

    public void Equip(in Weapon weapon, in EWeaponCategory targetCategory)
    {
        if (weapon.IsNotValid) return;

        RemoveItem(weapon);
        EquipmentInterface.Equip(weapon, targetCategory);
        AddStat(item.Definition.GetStat());
    }

    public void Unequip(in Weapon weapon, in EWeaponCategory targetCategory)
    {
        if (weapon.IsNotValid) return;

        AddItem(weapon);
        EquipmentInterface.Unequip(weapon, targetCategory);
        RemoveStat(item.Definition.GetStat());
    }

    public void Swap()
    {
        EquipmentInterface.Swap();
    }

    public Weapon HasWeapon(in EWeaponCategory targetCategory)
    {
        return EquipmentInterface.HasWeapon(targetCategory);
    }

    public void AddStat(IStat stat) => StatInterface.AddStat(stat);

    public void RemoveStat(IStat stat) => StatInterface.RemoveStat(stat);

    public void Attack(EWeaponCategory targetCategory)
    {
        var weaponSet = GetComponent<EquipmentComponent>();
        var weapon = weaponSet.HasWeapon(targetCategory);
        var skill = weapon.GetComponent<ISkill>();
        skill.Attack(StatInterface);
    }

    public void Damaged(IStatModifier modifier) => StatInterface.Damaged(modifier);

    /* MonoBehaviour */

    // TODO 테스트 코드이므로 제거하거나 별도의 컴포넌트로 분리가 필요
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        IInteractable InteractableInterface = collision.gameObject.GetComponent<IInteractable>();
        if(InteractableInterface == null) return;
        
        InteractableInterface.Interact(this);
    }
}
