using UnityEngine;
using UnityEngine.UI;
using Noong2.StatSystem;


#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 게임의 모든 UI를 관리하는 매니저 클래스
/// 체력바와 스탯창 UI의 생성, 초기화, 업데이트를 담당
/// </summary>
public class UIManager
{
    // 프리펩 경로
    private const string UI_PATH = "Assets/@Prefabs/UI";
    private const string HEALTH_BAR_PATH = UI_PATH + "/HealthBarUI.prefab";
    private const string STAT_WINDOW_PATH = UI_PATH + "/StatWindowUI.prefab";

    private HealthBarUI _healthBar;
    private StatWindowUI _statWindow;
    private StatComponent _playerStats;

    /// <summary>
    /// UI 매니저 초기화 - Canvas 생성 및 UI 프리팹들을 로드하여 초기화
    /// </summary>
    public void Init()
    {
        Debug.Log("UIManager Init 시작");

        // Canvas 찾기 또는 생성
        Canvas canvas = FindOrCreateCanvas();

        // 체력바 UI 프리팹 로드 및 생성
        GameObject healthBarPrefab = LoadPrefab(HEALTH_BAR_PATH);
        if (healthBarPrefab != null)
        {
            GameObject healthBarGo = Object.Instantiate(healthBarPrefab, canvas.transform); // Canvas의 자식으로 생성
            healthBarGo.name = "@UI_HealthBar";
            _healthBar = healthBarGo.GetComponent<HealthBarUI>();
            Debug.Log("HealthBarUI 로드 성공");
        }
        else
        {
            Debug.LogError($"HealthBarUI 프리팹을 찾을 수 없습니다! 경로: {HEALTH_BAR_PATH}");
        }

        // 스탯창 UI 프리팹 로드 및 생성
        GameObject statWindowPrefab = LoadPrefab(STAT_WINDOW_PATH);
        if (statWindowPrefab != null)
        {
            GameObject statWindowGo = Object.Instantiate(statWindowPrefab, canvas.transform);
            statWindowGo.name = "@UI_StatWindow";
            _statWindow = statWindowGo.GetComponent<StatWindowUI>();
            _statWindow.gameObject.SetActive(false);
            Debug.Log("StatWindowUI 로드 성공");
        }
        else
        {
            Debug.LogError($"StatWindowUI 프리팹을 찾을 수 없습니다! 경로: {STAT_WINDOW_PATH}");
        }

        Debug.Log("UIManager Init 완료");
    }

    // Canvas 찾거나 없으면 생성
    private Canvas FindOrCreateCanvas()
    {
        // 기존 Canvas 찾기
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();

        // Canvas가 없으면 생성
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("@UI_Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Canvas Scaler 추가
            CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            // Graphic Raycaster 추가
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        return canvas;
    }

    private GameObject LoadPrefab(string fullPath)
    {
#if UNITY_EDITOR
        // 현재 폴더 관리를 @로 하므로 Resources.Load사용 불가
        //빌드에서는 Resources 폴더를 사용하도록 변경해야 함
        return AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
#else
        return null;
#endif
    }

    public void InitializeStatWindow(StatComponent statComponent)
    {
        _playerStats = statComponent;
        UpdateStatWindow();
    }

    public void OnUpdate()
    {
        if (Managers.Input.IsStatWindowTogglePressed())
        {
            ToggleStatWindow();
        }
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (_healthBar != null)
        {
            _healthBar.UpdateHealth(currentHealth, maxHealth);
        }
    }

    private void ToggleStatWindow()
    {
        if (_statWindow != null)
        {
            _statWindow.Toggle();
            if (_statWindow.gameObject.activeSelf)
            {
                UpdateStatWindow();
            }
        }
    }

    public void RefreshStatWindow()
    {
        UpdateStatWindow();
    }

    private void UpdateStatWindow()
    {
        if (_playerStats != null && _statWindow != null)
        {
            _statWindow.UpdateStats(_playerStats);
        }
    }
}