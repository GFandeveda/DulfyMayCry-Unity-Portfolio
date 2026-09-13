using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject greenOrbPrefab;
    [SerializeField][Range(0, 100)] private int greenOrbChance = 10;

    [SerializeField] private GameObject redOrbPrefab;
    [SerializeField] private int redOrbsReward = 25;
    private RedOrbs playerRedOrbs;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private EnemyHealthUI healthUI;

    private int currentHealth;
    private EnemyController controller;

    private bool isDead;
    public bool IsDead => isDead;
    private EnemySpawner spawner;

    void Start()
    {
        spawner = FindFirstObjectByType<EnemySpawner>();
        playerRedOrbs = FindFirstObjectByType<RedOrbs>();
        currentHealth = maxHealth;
        controller = GetComponentInChildren<EnemyController>();

        healthUI.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        healthUI.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth > 0)
        {
            if (controller != null)
            {
                controller.PlayHit();
            }
        }
        else
        {
            Die();
        }
    }
    void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (spawner != null)
            spawner.EnemyDied(gameObject);

        if (controller != null)
            controller.PlayDeath();

        EnemyMovement melee = GetComponent<EnemyMovement>();
        if (melee != null)
            melee.enabled = false;

        Instantiate(redOrbPrefab, transform.position, Quaternion.identity);

        int random = Random.Range(0, 100);

        if (random < greenOrbChance)
        {
            Instantiate(greenOrbPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 4f);
    }
}