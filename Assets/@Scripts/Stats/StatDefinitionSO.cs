using UnityEngine;

/// <summary>
/// 개별 스탯의 기본 속성을 정의하는 ScriptableObject.
/// 스탯의 이름, 설명, 기본값, 최소/최대 범위를 설정.
/// </summary>
[CreateAssetMenu(fileName = "NewStat", menuName = "Stats/Stat Definition")]
public class StatDefinitionSO : ScriptableObject, IStat
{
    public string statName;        // 스탯 이름
    public string description;     // 스탯 설명
    public float defaultValue;     // 기본값
    public float minValue;         // 최소값
    public float maxValue;         // 최대값
    
    // IStat 인터페이스 구현
    public string StatName => statName;
    public string Description => description;
    public float DefaultValue => defaultValue;
    public float MinValue => minValue;
    public float MaxValue => maxValue;
} 