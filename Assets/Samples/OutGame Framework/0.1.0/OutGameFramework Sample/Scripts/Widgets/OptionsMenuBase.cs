using UnityEngine;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame.Sample
{
    public class OptionsMenuBase : UserWidget
    {
        [Header("References")]
        [SerializeField] protected Button m_ApplyButton;
        [SerializeField] protected Button m_ConfirmButton;

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

            if(m_ApplyButton != null) m_ApplyButton.onClick.AddListener(OnApplyButtonClicked);
            if(m_ConfirmButton != null) m_ConfirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        /* OptionsMenuBase */

        protected virtual void OnApplyButtonClicked()
        {
            LogOutGameFramework.Log("Apply Button Clicked");
        }

        protected virtual void OnConfirmButtonClicked()
        {
            LogOutGameFramework.Log("Confirm Button Clicked");

            Hide();
        }
    }
}
