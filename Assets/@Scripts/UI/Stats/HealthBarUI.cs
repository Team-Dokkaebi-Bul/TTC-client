using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;  // 체력바 배경
    [SerializeField] private Image _fillImage;        // 체력 게이지

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        //fillAmount(): Image.type 이 Image.Type.Filled 로 설정된 경우 표시되는 이미지의 양
        float fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        _fillImage.fillAmount = fillAmount;
    }
}