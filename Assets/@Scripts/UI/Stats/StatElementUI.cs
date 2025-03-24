using UnityEngine;
using TMPro;

/// <summary>
/// 개별 스탯 항목을 표시하는 UI 컴포넌트
/// 스탯 이름과 값을 한 쌍으로 표시
/// </summary>
public class StatElementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;  // 스탯 이름
    [SerializeField] private TextMeshProUGUI _statValueText; // 스탯 값

    public void Initialize(string statName, float value)
    {
        _statNameText.text = statName;
        _statValueText.text = value.ToString("F1");
    }
}