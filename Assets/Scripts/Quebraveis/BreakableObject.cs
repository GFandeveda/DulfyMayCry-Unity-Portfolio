using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private GameObject redOrbPrefab;
    [SerializeField] private Transform dropPoint;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Break();
    }

    private void Break()
    {
        if (redOrbPrefab != null)
        {
            Vector3 pos = dropPoint != null ? dropPoint.position : transform.position;
            Instantiate(redOrbPrefab, pos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}