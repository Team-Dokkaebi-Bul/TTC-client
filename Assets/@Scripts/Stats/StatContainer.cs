using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 하나의 개체가 가진 여러 스탯들을 관리하는 컨테이너.
/// 스탯의 추가, 조회, 수정 기능을 제공함.
/// </summary>
public class StatContainer
{
    #region Fields
    private readonly Dictionary<string, BasicStat> _stats = new();
    #endregion

    #region Stat Management
    public bool HasStat(string statName) => _stats.ContainsKey(statName);

    public float GetStatValue(string statName)
    {
        return _stats.TryGetValue(statName, out var stat) ? stat.CurrentValue : 0f;
    }

    public void SetStatValue(string statName, float value)
    {
        if (_stats.TryGetValue(statName, out var stat))
        {
            stat.BaseValue = value;
        }
    }

    public void AddStat(StatDefinitionSO statDefinition, float initialValue)
    {
        if (!_stats.ContainsKey(statDefinition.statName))
        {
            _stats[statDefinition.statName] = new BasicStat(statDefinition, initialValue);
        }
    }

    public Dictionary<string, float> GetAllStats()
    {
        Dictionary<string, float> allStats = new Dictionary<string, float>();
        foreach (var stat in _stats)
        {
            allStats[stat.Key] = stat.Value.CurrentValue;
        }
        return allStats;
    }
    #endregion
}