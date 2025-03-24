using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_Instance;
    public static Managers Instance { get { Init(); return s_Instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        Init();
        _ui.Init();
    }

    void Update()
    {
        _input.OnUpdate();
        _ui.OnUpdate();
    }

    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();
        }
    }
}
