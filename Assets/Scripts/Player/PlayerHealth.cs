using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool canPlayHit = true;

    private int baseMaxHealth;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private PlayerHealthUI healthUI;
    private Animator animator;

    private int currentHealth;

    [SerializeField] private float invincibilityTime = 0.5f;

    private bool isInvincible;
    private float invincibilityTimer;
    private bool isDead;

    void Start()
    {
        baseMaxHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;

        healthUI.UpdateHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (!isInvincible)
            return;
        PlayerCombat combat = GetComponent<PlayerCombat>();

        if (combat.IsDodging)
            return;
        invincibilityTimer -= Time.deltaTime;

        if (invincibilityTimer <= 0)
        {
            isInvincible = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        if (isInvincible)
            return;
        PlayerCombat combat = GetComponent<PlayerCombat>();

        if (combat.IsBlocking)
        {
            Debug.Log("Ataque bloqueado!");
            return;
        }

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        healthUI.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        animator.SetTrigger("Hit");

        isInvincible = true;
        invincibilityTimer = invincibilityTime;
    }
        void Die()
    {
        if (isDead)
            return;

        isDead = true;

        animator.SetTrigger("Die");

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;

        healthUI.gameObject.SetActive(false);

        Debug.Log("Player morreu");
    }
    public bool IsDead => isDead;

    public void Heal(int amount)
    {
        if (isDead)
            return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthUI.UpdateHealth(currentHealth, maxHealth);

        Debug.Log("Curou " + amount + " de vida.");
    }

    public void IncreaseMaxHealthByPercent(float percent)
    {
        int increase = Mathf.RoundToInt(maxHealth * percent);

        maxHealth += increase;
        currentHealth += increase;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthUI.UpdateHealth(currentHealth, maxHealth);

        Debug.Log($"Vida máxima aumentada em {increase}.");
    }
    public void IncreaseMaxHealth()
    {
        Debug.Log("IncreaseMaxHealth chamado");
        

        int increase = Mathf.RoundToInt(baseMaxHealth * 0.10f);

        maxHealth += increase;
        currentHealth += increase;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthUI.UpdateHealth(currentHealth, maxHealth);
        healthUI.IncreaseBarSize(20f);

        Debug.Log("Vida máxima aumentada em " + increase + ".");
    }
}