using UnityEngine;
using System.Collections.Generic;
using Noong2.StatSystem;


public class WeaponMasteryComponent : MonoBehaviour
{
    private Dictionary<WeaponClass, float> _masteryLevels = new Dictionary<WeaponClass, float>();
    private Dictionary<WeaponClass, bool> _isMastered = new Dictionary<WeaponClass, bool>();
    private List<StatModifier> _activeMasteryModifiers = new List<StatModifier>();
    private StatComponent _stats;

    private void Awake()
    {
        _stats = GetComponent<StatComponent>();

        foreach (WeaponClass type in System.Enum.GetValues(typeof(WeaponClass)))
        {
            _masteryLevels[type] = 0f;
            _isMastered[type] = false;
        }
    }

    public void IncreaseMastery(WeaponClass type, float amount)
    {
        if (!_masteryLevels.ContainsKey(type))
            _masteryLevels[type] = 0f;

        bool wasMastered = _isMastered[type];

        _masteryLevels[type] = Mathf.Clamp01(_masteryLevels[type] + amount);

        // test log
        Debug.Log($"{type} 무기 숙련도: {_masteryLevels[type] * 100:F1}%");

        _isMastered[type] = _masteryLevels[type] >= 1.0f;

        if (!wasMastered && _isMastered[type])
        {
            Debug.Log($"<color=yellow>[마스터 달성!] {type} 무기 숙련도 100% 달성!</color>");

            TestWarrior player = GetComponent<TestWarrior>();
            if (player != null && player.GetEquippedWeapon() != null &&
                player.GetEquippedWeapon().GetWeaponClass() == type)
            {
                ApplyMasteryBonus(type);
            }
        }
    }

    public void OnWeaponEquipped(WeaponItem weapon)
    {
        RemoveMasteryBonus();

        WeaponClass type = weapon.GetWeaponClass();

        // test log
        Debug.Log($"{type} 무기 장착 - 현재 숙련도: {_masteryLevels[type] * 100:F1}%");

        if (_isMastered[type])
        {
            ApplyMasteryBonus(type);
        }
    }

    public void OnWeaponUnequipped()
    {
        RemoveMasteryBonus();
    }

    private void ApplyMasteryBonus(WeaponClass type)
    {
        RemoveMasteryBonus();

        Dictionary<string, float> bonusStats = new Dictionary<string, float>();

        switch (type)
        {
            case WeaponClass.Blunt:
                bonusStats["Strength"] = 5f;
                break;
            case WeaponClass.Sword:
                bonusStats["Agility"] = 5f;
                break;
            case WeaponClass.Bow:
                bonusStats["Luck"] = 5f;
                break;
        }

        foreach (var stat in bonusStats)
        {
            if (_stats.HasStat(stat.Key))
            {
                var modifier = new StatModifier(stat.Key, ModifierType.Flat, stat.Value, _stats);
                _activeMasteryModifiers.Add(modifier);
                modifier.Apply();

                Debug.Log($"<color=green>{type} 무기 마스터리 보너스: {stat.Key} +{stat.Value} 적용됨</color>");
            }
        }

        TestWarrior player = GetComponent<TestWarrior>();
        if (player != null)
        {
            player.UpdateStatWindowUI();
        }
    }

    private void RemoveMasteryBonus()
    {
        if (_activeMasteryModifiers.Count > 0)
        {
            foreach (var modifier in _activeMasteryModifiers)
            {
                modifier.Remove();
            }

            _activeMasteryModifiers.Clear();

            TestWarrior player = GetComponent<TestWarrior>();
            if (player != null)
            {
                player.UpdateStatWindowUI();
            }

            Debug.Log("무기 마스터리 보너스가 제거되었습니다.");
        }
    }
}