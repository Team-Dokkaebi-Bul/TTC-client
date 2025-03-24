using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 개별 스탯의 실제 값을 관리하는 클래스.
/// 스탯의 기본값과 현재값을 관리하며, 값의 범위를 제한함.
/// </summary>
public class BasicStat
{
    #region Fields
    private StatDefinitionSO _definition;
    private float _baseValue;
    #endregion

    #region Properties
    public float BaseValue
    {
        get => _baseValue;
        set => _baseValue = value;
    }

    public float CurrentValue => Mathf.Clamp(_baseValue, _definition.minValue, _definition.maxValue);
    #endregion

    public BasicStat(StatDefinitionSO definition, float initialValue)
    {
        _definition = definition;
        _baseValue = initialValue;
    }
}