using UnityEngine;

public class EnemySwordHitbox : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private BoxCollider hitbox;
    private bool hasHitPlayer;

    void Awake()
    {
        hitbox = GetComponent<BoxCollider>();
        hitbox.enabled = false;
    }

    public void EnableHitbox()
    {
        hasHitPlayer = false;
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHitPlayer)
            return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player == null)
            return;

        hasHitPlayer = true;

        player.TakeDamage(damage);
    }
}