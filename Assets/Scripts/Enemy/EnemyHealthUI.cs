using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Image damageFillImage;

    [SerializeField] private float greenSpeed = 10f;
    [SerializeField] private float redSpeed = 2f;

    private float targetFill = 1f;

    void Start()
    {
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
}