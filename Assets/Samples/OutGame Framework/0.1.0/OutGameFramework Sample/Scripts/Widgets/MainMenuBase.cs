using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame.Sample
{
    /// <summary>
    /// 일반적인 게임들의 메인 메뉴 위젯 클래스입니다.
    /// </summary>
    public class MainMenuBase : UserWidget
    {
        [Header("References")]
        [SerializeField] protected Button m_PlayButton;
        [SerializeField] protected Button m_OptionsButton;
        [SerializeField] protected Button m_ExitButton;

        [Header("Config")]
        [SerializeField] SampleSceneType m_MainScene = SampleSceneType.Main;

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

            // Bind button events
            if (m_PlayButton != null) m_PlayButton.onClick.AddListener(OnPlayButtonClicked);
            if (m_OptionsButton != null) m_OptionsButton.onClick.AddListener(OnOptionsButtonClicked);
            if (m_ExitButton != null) m_ExitButton.onClick.AddListener(OnExitButtonClicked);
        }

        /* MainMenuBase */

        public virtual void OnPlayButtonClicked()
        {
            LogOutGameFramework.Log("Play Button Clicked");
            OpenMainScene();
        }

        public virtual void OnOptionsButtonClicked()
        {
            LogOutGameFramework.Log("Options Button Clicked");
            OutGameFrameworkFunctionLibrary.ShowOptionsWidget();
        }

        public virtual void OnExitButtonClicked()
        {
            LogOutGameFramework.Log("Exit Button Clicked");

            ModalRequestData exitRequestData = new ModalRequestData
            {
                Title = "Exit Game",
                Message = "Are you sure you want to exit?",
                Confirmed = OutGameFrameworkFunctionLibrary.Exit
            };
            RequestConfirm(exitRequestData);
        }

        protected virtual void OpenMainScene()
        {
            SceneLoadingManager.Instance.LoadScene(m_MainScene);
        }
    }
}
