using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform backgroundRect;

    [SerializeField] private Image fillImage;
    [SerializeField] private Image damageFillImage;

    [SerializeField] private float greenSpeed = 10f;
    [SerializeField] private float redSpeed = 2f;

    private float targetFill;

    void Start()
    {
        targetFill = 1f;

        fillImage.fillAmount = 1f;
        damageFillImage.fillAmount = 1f;
    }

    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(
            fillImage.fillAmount,
            targetFill,
            greenSpeed * Time.deltaTime);

        damageFillImage.fillAmount = Mathf.Lerp(
            damageFillImage.fillAmount,
            targetFill,
            redSpeed * Time.deltaTime);
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        targetFill = (float)currentHealth / maxHealth;
    }

    public void IncreaseBarSize(float amount)
    {
        Vector2 size = backgroundRect.sizeDelta;
        size.x += amount;
        backgroundRect.sizeDelta = size;
    }
}