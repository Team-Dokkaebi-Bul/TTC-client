using UnityEngine;
using System;
using System.Collections.Generic;

// 캐릭터나 아이템의 기본 스탯 설정을 정의
[CreateAssetMenu(fileName = "NewStatPreset", menuName = "Stats/Stat Preset")]
public class StatPresetSO : ScriptableObject
{
    [Serializable]
    public class StatValue
    {
        public StatDefinitionSO stat;    // 스탯 정의
        public float value;              // 초기값
    }

    public List<StatValue> stats = new();
} 