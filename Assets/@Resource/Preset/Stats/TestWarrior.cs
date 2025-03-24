using UnityEngine;
using System.Text;

public class TestWarrior : MonoBehaviour
{
    #region Fields
    private StatModifier _currentModifier;
    private StatComponent _stats;
    private WeaponItem _equippedWeapon;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _stats = GetComponent<StatComponent>();

        // UI 초기화
        Managers.UI.InitializeStatWindow(_stats);
        UpdateHealthBarUI();

        Debug.Log($"[{gameObject.name}] 워리어 스탯 초기화 완료:" +
                  $"\n체력: {GetHealth()}" +
                  $"\n체력 최대값: {_stats.GetStatPreset().stats.Find(x => x.stat.statName == "Health").stat.maxValue}" +
                  $"\n마나: {GetMana()}");
    }

    private void Update()
    {
        // 'W' 키를 누르면 무기 장착/해제 토글 (테스트용)
        if (Input.GetKeyDown(KeyCode.W))
        {
            ToggleWeapon();
        }
    }
    #endregion

    #region Weapon Methods
    public void EquipWeapon(WeaponItem weapon)
    {
        if (_equippedWeapon != null)
        {
            UnequipWeapon();
        }

        _equippedWeapon = weapon;
        _equippedWeapon.OnEquip(this);
        Debug.Log("플레이어가 무기를 장착함.");
    }

    public void UnequipWeapon()
    {
        if (_equippedWeapon != null)
        {
            _equippedWeapon.OnUnequip();
            _equippedWeapon = null;
        }
        Debug.Log("플레이어가 무기를 해제함.");
    }

    private void ToggleWeapon()
    {
        if (_equippedWeapon != null)
        {
            UnequipWeapon();
        }
        else
        {
            // 테스트용: 주변에서 무기 찾기
            var weapon = Object.FindFirstObjectByType<WeaponItem>();
            if (weapon != null)
            {
                EquipWeapon(weapon);
            }
        }
    }
    #endregion

    #region Stat Access Methods
    public float GetHealth() => _stats.GetStatValue("Health");
    public float GetMana() => _stats.GetStatValue("Mana");
    public float GetStatValue(string statName) => _stats.GetStatValue(statName);
    public void SetStatValue(string statName, float value) => _stats.SetStatValue(statName, value);
    public float GetMaxHealth()
    {
        var healthStat = _stats.GetStatPreset().stats.Find(x => x.stat.statName == "Health");
        return healthStat != null ? healthStat.stat.maxValue : 0f;
    }
    #endregion

    #region Damage Handling
    public void BeAttacked(float damage, StatModifier modifier)
    {
        if (modifier != null)
        {
            _currentModifier = modifier;
            _currentModifier.Apply();
        }

        float currentHealth = GetHealth();
        float newHealth = currentHealth - damage;
        _stats.SetStatValue("Health", newHealth);

        // UI 업데이트
        UpdateHealthBarUI();
        UpdateStatWindowUI();

        Debug.Log($"플레이어가 {damage}의 데미지를 받았습니다. 남은 체력: {newHealth}");

        if (newHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log("플레이어가 사망했습니다!");
        gameObject.SetActive(false);
    }
    #endregion

    #region UI Methods
    private void UpdateHealthBarUI()
    {
        float currentHealth = GetHealth();
        float maxHealth = GetMaxHealth();
        Managers.UI.UpdateHealthBar(currentHealth, maxHealth);

        Debug.Log($"체력바 업데이트: {currentHealth}/{maxHealth}");
    }

    public void UpdateStatWindowUI()
    {
        if (_stats != null)
        {
            Managers.UI.RefreshStatWindow();
        }
    }
    #endregion
}
