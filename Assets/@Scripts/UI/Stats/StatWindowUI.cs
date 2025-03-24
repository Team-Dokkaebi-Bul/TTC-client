using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 캐릭터의 전체 스탯을 표시하는 창 UI
/// 토글 방식으로 표시/숨김 가능
/// </summary>
public class StatWindowUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _statContainer;           // 스탯 요소들의 부모 Transform
    [SerializeField] private StatElementUI _statElementPrefab;   // 개별 스탯 요소 프리팹

    private Dictionary<string, StatElementUI> _statElements = new Dictionary<string, StatElementUI>();

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void UpdateStats(StatComponent statComponent)
    {
        // 기존 스탯 요소들 제거(스탯이 변경될 때 대응)
        foreach (Transform child in _statContainer)
        {
            Destroy(child.gameObject);
        }
        _statElements.Clear();

        // 모든 스탯에 대해 UI 요소 생성
        var stats = statComponent.GetAllStats();
        foreach (var stat in stats)
        {
            StatElementUI element = Instantiate(_statElementPrefab, _statContainer);
            element.Initialize(stat.Key, stat.Value);
            _statElements[stat.Key] = element;
        }
    }
}