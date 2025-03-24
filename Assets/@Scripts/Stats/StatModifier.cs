using UnityEngine;

/// <summary>
/// 스탯 수정자의 적용 방식을 정의하는 열거형입니다.
/// </summary>
public enum ModifierType
{
    Flat,           // 고정값 증감 (예: +10)
    PercentAdd,     // 퍼센트 증가(가산) (예: +10% + 20% = +30%)
    PercentMult     // 퍼센트 증가(승산) (예: 1.1 * 1.2 = 1.32)
}

public class StatModifier
{
    #region Fields
    private readonly string _targetStatName;
    private readonly ModifierType _type;
    private readonly float _value;
    private readonly StatComponent _target;
    private bool _isActive;
    #endregion

    #region Constructor
    public StatModifier(string targetStatName, ModifierType type, float value, StatComponent target)
    {
        _targetStatName = targetStatName;
        _type = type;
        _value = value;
        _target = target;
    }
    #endregion

    #region Public Methods
    public void Apply()
    {
        if (_isActive) return;
        _isActive = true;

        float currentValue = _target.GetStatValue(_targetStatName);
        float newValue = CalculateNewValue(currentValue);
        _target.SetStatValue(_targetStatName, newValue);
    }

    public void Remove()
    {
        if (!_isActive) return;

        float currentValue = _target.GetStatValue(_targetStatName);
        float originalValue = CalculateOriginalValue(currentValue);
        _target.SetStatValue(_targetStatName, originalValue);

        _isActive = false;
    }
    #endregion

    #region Private Methods
    private float CalculateNewValue(float baseValue)
    {
        return _type switch
        {
            ModifierType.Flat => baseValue + _value,
            ModifierType.PercentAdd => baseValue * (1 + _value),
            ModifierType.PercentMult => baseValue * _value,
            _ => baseValue
        };
    }

    private float CalculateOriginalValue(float baseValue)
    {
        return _type switch
        {
            ModifierType.Flat => baseValue - _value,
            ModifierType.PercentAdd => baseValue / (1 + _value),
            ModifierType.PercentMult => baseValue / _value,
            _ => baseValue
        };
    }
    #endregion
}

