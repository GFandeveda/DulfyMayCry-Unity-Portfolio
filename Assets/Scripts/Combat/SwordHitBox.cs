using UnityEngine;
using System.Collections.Generic;

public class SwordHitbox : MonoBehaviour
{
    [SerializeField] private AudioClip hitClip;
    private AudioSource audioSource;

    [SerializeField] private int damage = 25;

    private BoxCollider hitbox;
    private List<EnemyHealth> enemiesHit = new List<EnemyHealth>();


    void Awake()
    {
        hitbox = GetComponent<BoxCollider>();

        hitbox.enabled = false;
    }
    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            return;
        }

        BreakableObject breakable = other.GetComponent<BreakableObject>();

        if (breakable != null)
        {
            breakable.TakeDamage(damage);
        }
    }

    public void ResetHitbox()
    {
        enemiesHit.Clear();
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}