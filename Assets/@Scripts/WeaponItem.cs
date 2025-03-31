using UnityEngine;
using System.Collections.Generic;
using Weapon;

public class WeaponItem : MonoBehaviour
{
    #region Fields
    [SerializeField] private IStatComponent _weaponStats;
    [SerializeField] private ISkillComponent _weaponSkills;
    [SerializeField] private WeaponClass _weaponClass;  // 무기 타입
    private TestWarrior _equippedPlayer;
    private List<StatModifier> _activeModifiers = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (_weaponStats == null)
        {
            _weaponStats = GetComponent<IStatComponent>();
            if (_weaponStats == null)
            {
                Debug.LogError($"IStatComponent가 {gameObject.name}에 없습니다!");
                return;
            }
        }

        if (_weaponSkills == null)
        {
            _weaponSkills = GetComponent<ISkillComponent>();
            if (_weaponSkills == null)
            {
                Debug.LogError($"ISkillComponent가 {gameObject.name}에 없습니다!");
                return;
            }
        }
    }
    #endregion

    #region Public Methods
    public void OnEquip(TestWarrior player)
    {
        if (_equippedPlayer != null)
        {
            Debug.LogWarning($"무기가 이미 {_equippedPlayer.name}에 장착되어 있습니다!");
            return;
        }

        _equippedPlayer = player;
        ApplyWeaponStatsToPlayer();
        _weaponSkills.EnableSkills(player);
        Debug.Log($"[{gameObject.name}] {player.name}에게 장착됨");
    }

    public void OnUnequip()
    {
        if (_equippedPlayer == null) return;

        RemoveWeaponStatsFromPlayer();
        _weaponSkills.DisableSkills();
        _equippedPlayer = null;
        Debug.Log($"[{gameObject.name}] 장착 해제됨");
    }

    // 무기 타입 반환
    public WeaponClass GetWeaponClass()
    {
        return _weaponClass;
    }
    #endregion

    #region Private Methods
    private void ApplyWeaponStatsToPlayer()
    {
        if (_equippedPlayer == null) return;

        // 무기의 모든 스탯을 플레이어에게 적용
        var weaponStats = _weaponStats.GetAllStats();
        var playerStatComponent = _equippedPlayer.GetStatComponent();
        
        foreach (var stat in weaponStats)
        {
            var statDefinition = _weaponStats.GetStatDefinition(stat.Key);
            if (statDefinition != null)
            {
                var modifier = new StatModifier(stat.Key, ModifierType.Flat, stat.Value, playerStatComponent);
                _activeModifiers.Add(modifier);
                modifier.Apply();
            }
        }

        _equippedPlayer.UpdateStatWindowUI();
    }

    private void RemoveWeaponStatsFromPlayer()
    {
        if (_equippedPlayer == null) return;

        // 모든 활성 수정자 제거
        foreach (var modifier in _activeModifiers)
        {
            modifier.Remove();
        }
        _activeModifiers.Clear();

        _equippedPlayer.UpdateStatWindowUI();
    }
    #endregion
}