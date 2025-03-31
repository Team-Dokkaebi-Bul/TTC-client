using Eu4ng.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Weapon;
using System.Collections.Generic;

/// <summary>
/// 플레이어 캐릭터 컨테이너 클래스
/// </summary>
[RequireComponent(typeof(StatComponent), typeof(WeaponSlot))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField, ReadOnly] PlayerData m_PlayerData;
    
    // 스탯 컴포넌트 참조
    private StatComponent _statComponent;
    
    /* Properties */

    public IInventoryComponent InventoryInterface => null;
    public IEquipmentComponent EquipmentInterface => null;
    public IStatComponent StatInterface => _statComponent;
    
    private void Awake()
    {
        _statComponent = GetComponent<StatComponent>();
    }
    
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

    public void AttackByMainWeapon()
    {
        var weaponSlot = GetComponent<WeaponSlot>();
        var weapon = weaponSlot.GetCurrentMainWeapon();
        //var skill = weapon.GetComponent<ISkill>();
        //skill.Attack(StatInterface);
    }

    public void AttackBySubWeapon()
    {
        var weaponSlot = GetComponent<WeaponSlot>();
        var weapon = weaponSlot.GetCurrentSubWeapon();
        //var skill = weapon.GetComponent<ISkill>();
        //skill.Attack(StatInterface);
    }

    public void Damaged(IStatModifier modifier) => StatInterface.Damaged(modifier);

    #region IStatComponent 인터페이스
    public bool HasStat(string statName) => StatInterface.HasStat(statName);
    
    public float GetStatValue(string statName) => StatInterface.GetStatValue(statName);
    
    public void SetStatValue(string statName, float value) => StatInterface.SetStatValue(statName, value);
    
    public IStat GetStatDefinition(string statName) => StatInterface.GetStatDefinition(statName);
    
    public Dictionary<string, float> GetAllStats() => StatInterface.GetAllStats();
    
    public void AddStat(IStat stat, float initialValue = float.MinValue) => StatInterface.AddStat(stat, initialValue);

    public void RemoveStat(IStat stat) => StatInterface.RemoveStat(stat);
        
    #endregion
    

    /* MonoBehaviour */

    // TODO 테스트 코드이므로 제거하거나 별도의 컴포넌트로 분리가 필요
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        IInteractable InteractableInterface = collision.gameObject.GetComponent<IInteractable>();
        if(InteractableInterface == null) return;
        
        InteractableInterface.Interact(this);
    }
}
