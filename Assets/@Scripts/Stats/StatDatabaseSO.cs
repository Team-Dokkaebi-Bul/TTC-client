using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 게임에서 사용 가능한 모든 스탯을 관리하는 중앙 저장소
/// 모든 스탯 정의를 보관하고 이름으로 검색할 수 있게 해줌.
/// </summary>
[CreateAssetMenu(fileName = "StatDatabase", menuName = "Stats/Stat Database")]
public class StatDatabaseSO : ScriptableObject
{
    #region Fields
    public List<StatDefinitionSO> availableStats = new();
    private Dictionary<string, StatDefinitionSO> _statLookup;
    #endregion

    #region Initialization
    public void Initialize()
    {
        _statLookup = new Dictionary<string, StatDefinitionSO>();
        foreach (var stat in availableStats)
        {
            _statLookup[stat.statName] = stat;
        }
    }
    #endregion

    #region Stat Access
    public StatDefinitionSO GetStatDefinition(string statName)
    {
        return _statLookup.TryGetValue(statName, out var stat) ? stat : null;
    }
    #endregion
} 