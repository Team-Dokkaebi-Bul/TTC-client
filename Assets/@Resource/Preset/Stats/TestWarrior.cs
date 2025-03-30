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

        // UI 초기화 - 스탯 창만 초기화
        Managers.UI.InitializeStatWindow(_stats);
    }

    private void Start()
    {
        // 스탯 초기화 이후에 UI 업데이트 및 로그 출력
        UpdateHealthBarUI();
        
        Debug.Log($"[{gameObject.name}] 워리어 스탯 초기화 완료:" +
                  $"\n체력: {GetHealth()}" +
                  $"\n체력 최대값: {GetMaxHealth()}" +
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

        var mastery = GetComponent<WeaponMasteryComponent>();
        if (mastery != null)
        {
            mastery.OnWeaponEquipped(weapon);
        }

        Debug.Log("플레이어가 무기를 장착함.");
    }

    public void UnequipWeapon()
    {
        if (_equippedWeapon != null)
        {
            _equippedWeapon.OnUnequip();

            var mastery = GetComponent<WeaponMasteryComponent>();
            if (mastery != null)
            {
                mastery.OnWeaponUnequipped();
            }

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
            var weapon = Object.FindFirstObjectByType<WeaponItem>();
            if (weapon != null)
            {
                EquipWeapon(weapon);
            }
        }
    }

    // 무기 반환 메서드 추가
    public WeaponItem GetEquippedWeapon()
    {
        return _equippedWeapon;
    }
    #endregion

    #region Stat Access Methods
    public float GetHealth() => _stats.GetStatValue("Health");
    public float GetMana() => _stats.GetStatValue("Mana");
    public float GetStatValue(string statName) => _stats.GetStatValue(statName);
    public void SetStatValue(string statName, float value) => _stats.SetStatValue(statName, value);
    
    public float GetMaxHealth()
    {
        // 새로 추가한 GetStatDefinition 메서드를 사용
        var healthDef = _stats.GetStatDefinition("Health");
        return healthDef != null ? healthDef.maxValue : 100f; // 없을 경우 기본값 100 사용
    }
    
    // 스탯 컴포넌트 직접 접근 메서드 추가
    public StatComponent GetStatComponent()
    {
        return _stats;
    }
    #endregion

    #region Damage Handling
    public void BeAttacked(float damage, StatModifier modifier)
    {
        // 상태이상 효과 처리
        if (modifier != null)
        {
            _currentModifier = modifier;
            _currentModifier.Apply();
        }

        // 데미지 처리
        float currentHealth = GetHealth();
        float newHealth = currentHealth - damage;
        _stats.SetStatValue("Health", newHealth);

        // UI 업데이트
        UpdateHealthBarUI();
        UpdateStatWindowUI();  // 스탯창도 업데이트

        Debug.Log($"플레이어가 {damage}의 데미지를 받았습니다. 남은 체력: {newHealth}");

        // 사망 처리
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
