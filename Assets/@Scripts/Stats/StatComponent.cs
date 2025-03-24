using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 스탯을 관리하는 컴포넌트입니다.
/// 게임 오브젝트에 부착하여 스탯 기능을 제공합니다.
/// </summary>
public class StatComponent : MonoBehaviour
{
    #region Fields
    [SerializeField] private StatPresetSO _statPreset;  // 초기 스탯 설정
    private StatContainer _stats = new();                // 스탯 저장소
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (_statPreset == null)
        {
            Debug.LogError($"StatPreset이 {gameObject.name}에 설정되지 않았습니다!");
            return;
        }

        InitializeStats();
    }
    #endregion

    #region Initialization
    private void InitializeStats()
    {
        if (_statPreset != null)
        {
            foreach (var statValue in _statPreset.stats)
            {
                _stats.AddStat(statValue.stat, statValue.value);
            }
        }
    }
    #endregion

    #region Stat Access
    public bool HasStat(string statName) => _stats.HasStat(statName);
    public float GetStatValue(string statName) => _stats.GetStatValue(statName);
    
    public void SetStatValue(string statName, float value)
    {
        if (_stats.HasStat(statName))
        {
            _stats.SetStatValue(statName, value);
        }
        else
        {
            Debug.LogWarning($"Stat {statName} 스탯 찾을 수 없음 {gameObject.name}");
        }
    }

    public StatPresetSO GetStatPreset()
    {
        return _statPreset;
    }

    public void SetStatPreset(StatPresetSO preset)
    {
        _statPreset = preset;
        InitializeStats();
    }

    public Dictionary<string, float> GetAllStats()
    {
        return _stats.GetAllStats();
    }
    #endregion
} 