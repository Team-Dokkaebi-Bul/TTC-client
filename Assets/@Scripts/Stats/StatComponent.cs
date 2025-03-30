using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 스탯을 관리하는 컴포넌트입니다.
/// 게임 오브젝트에 부착하여 스탯 기능을 제공합니다.
/// </summary>
public class StatComponent : MonoBehaviour
{
    #region Internal Classes
    [Serializable]
    public class StatData
    {
        public StatDefinitionSO definition;
        public float baseValue;
    }
    #endregion

    #region Fields
    [SerializeField] private List<StatData> _initialStats = new();
    private Dictionary<string, StatData> _stats = new();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        InitializeStats();
    }
    #endregion

    #region Initialization
    private void InitializeStats()
    {
        Debug.Log($"[{gameObject.name}] 스탯 초기화 시작: {_initialStats.Count}개의 스탯");
        
        foreach (var statData in _initialStats)
        {
            if (statData.definition != null)
            {
                string statName = statData.definition.statName;
                float value = statData.baseValue != 0 ? statData.baseValue : statData.definition.defaultValue;
                
                var newStatData = new StatData
                {
                    definition = statData.definition,
                    baseValue = value
                };
                
                _stats[statName] = newStatData;
                Debug.Log($"[{gameObject.name}] 스탯 추가: {statName} = {value}");
            }
            else
            {
                Debug.LogWarning($"[{gameObject.name}] 스탯 정의가 null입니다!");
            }
        }
        
        // 초기화된 모든 스탯 출력
        string statsDebug = "초기화된 스탯 목록: ";
        foreach (var stat in _stats)
        {
            statsDebug += $"{stat.Key}={stat.Value.baseValue}, ";
        }
        Debug.Log($"[{gameObject.name}] {statsDebug}");
    }
    #endregion

    #region Stat Access
    public bool HasStat(string statName) => _stats.ContainsKey(statName);
    
    public float GetStatValue(string statName)
    {
        if (_stats.TryGetValue(statName, out var stat))
        {
            // 최소/최대값 범위 내로 제한
            return Mathf.Clamp(stat.baseValue, stat.definition.minValue, stat.definition.maxValue);
        }
        
        // 사용 가능한 스탯 이름들을 표시하여 디버깅 개선
        string availableStats = string.Join(", ", _stats.Keys);
        Debug.LogWarning($"스탯 {statName}을(를) 찾을 수 없음: {gameObject.name}. 사용 가능한 스탯: {availableStats}");
        return 0f;
    }
    
    public void SetStatValue(string statName, float value)
    {
        if (_stats.TryGetValue(statName, out var stat))
        {
            stat.baseValue = value;
        }
        else
        {
            Debug.LogWarning($"스탯 {statName}을(를) 찾을 수 없음: {gameObject.name}");
        }
    }
    
    // 스탯 정의 정보 가져오기
    public StatDefinitionSO GetStatDefinition(string statName)
    {
        if (_stats.TryGetValue(statName, out var stat))
        {
            return stat.definition;
        }
        
        Debug.LogWarning($"스탯 {statName}의 정의를 찾을 수 없음: {gameObject.name}");
        return null;
    }
    
    public Dictionary<string, float> GetAllStats()
    {
        Dictionary<string, float> allStats = new Dictionary<string, float>();
        foreach (var stat in _stats)
        {
            allStats[stat.Key] = GetStatValue(stat.Key);
        }
        return allStats;
    }
    
    public void AddStat(StatDefinitionSO definition, float initialValue = float.MinValue)
    {
        if (definition == null) return;
        
        string statName = definition.statName;
        if (!_stats.ContainsKey(statName))
        {
            float value = initialValue != float.MinValue ? initialValue : definition.defaultValue;
            
            var newStatData = new StatData
            {
                definition = definition,
                baseValue = value
            };
            
            _stats[statName] = newStatData;
        }
    }
    
    // 스탯 제거 메서드
    public bool RemoveStat(string statName)
    {
        if (_stats.ContainsKey(statName))
        {
            _stats.Remove(statName);
            return true;
        }
        
        Debug.LogWarning($"제거할 스탯 {statName}을(를) 찾을 수 없음: {gameObject.name}");
        return false;
    }
    #endregion
} 