using UnityEngine;
using System.Collections.Generic;

public class WeaponItem : MonoBehaviour
{
    #region Fields
    [SerializeField] private StatComponent _weaponStats;
    [SerializeField] private SkillComponent _weaponSkills;
    private TestWarrior _equippedPlayer;
    private List<StatModifier> _activeModifiers = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (_weaponStats == null)
        {
            _weaponStats = GetComponent<StatComponent>();
            if (_weaponStats == null)
            {
                Debug.LogError($"StatComponent가 {gameObject.name}에 없습니다!");
                return;
            }
        }

        if (_weaponSkills == null)
        {
            _weaponSkills = GetComponent<SkillComponent>();
            if (_weaponSkills == null)
            {
                Debug.LogError($"SkillComponent가 {gameObject.name}에 없습니다!");
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
    #endregion

    #region Private Methods
    private void ApplyWeaponStatsToPlayer()
    {
        if (_equippedPlayer == null) return;

        var weaponStats = _weaponStats.GetAllStats();
        foreach (var stat in weaponStats)
        {
            var modifier = new StatModifier(stat.Key, ModifierType.Flat, stat.Value, _equippedPlayer.GetComponent<StatComponent>());
            _activeModifiers.Add(modifier);
            modifier.Apply();
        }

        _equippedPlayer.UpdateStatWindowUI();
    }

    private void RemoveWeaponStatsFromPlayer()
    {
        if (_equippedPlayer == null) return;

        foreach (var modifier in _activeModifiers)
        {
            modifier.Remove();
        }
        _activeModifiers.Clear();

        _equippedPlayer.UpdateStatWindowUI();
    }
    #endregion
}