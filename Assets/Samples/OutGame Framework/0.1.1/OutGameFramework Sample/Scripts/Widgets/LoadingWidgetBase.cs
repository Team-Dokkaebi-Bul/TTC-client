using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame.Sample
{
    public class LoadingWidgetBase : UserWidget, ILoadingWidget
    {
        [Header("References")]
        [SerializeField] Image m_FadingImage;
        [SerializeField] RectTransform m_LoadingScreen;
        [SerializeField] TextMeshProUGUI m_LoadingStateText;
        [SerializeField] TextMeshProUGUI m_LoadingPercentageText;
        [SerializeField] Slider m_LoadingPercentageSlider;

        float m_LoadingProgress = -1;

        /* ILoadingWidget */
        public virtual void FadeOut(float duration)
        {
            LogOutGameFramework.Log("Start FadeOut");
            var originalColor = m_FadingImage.color;
            m_FadingImage.CrossFadeAlpha(0, 0, true);
            m_FadingImage.CrossFadeAlpha(1, duration, true);
        }

        public virtual void FadeIn(float duration)
        {
            LogOutGameFramework.Log("Start FadeIn");
            var originalColor = m_FadingImage.color;
            m_FadingImage.CrossFadeAlpha(0, 1, true);
            m_FadingImage.CrossFadeAlpha(0, duration, true);
        }

        public virtual void ShowLoadingScreen() => m_LoadingScreen.gameObject.SetActive(true);

        public virtual void HideLoadingScreen() => m_LoadingScreen.gameObject.SetActive(false);

        public virtual void UpdateLoadingProgress(float progress)
        {
            if (Mathf.Approximately(m_LoadingProgress, progress)) return;
            m_LoadingProgress = progress;

            LogOutGameFramework.Log("LoadingProgress: " + m_LoadingProgress);

            // Update LoadingPercentageSlider
            if (m_LoadingPercentageSlider != null)
            {
                m_LoadingPercentageSlider.value = progress;
            }

            // Update LoadingPercentageText
            if (m_LoadingPercentageText != null)
            {
                double percentage = Math.Round(progress * 100f, 1);
                m_LoadingPercentageText.text = percentage + "%";
            }
        }

        public virtual void UpdateLoadingState(string state) => m_LoadingStateText?.SetText(state);
    }
}
