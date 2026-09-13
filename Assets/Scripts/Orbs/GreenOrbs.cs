using UnityEngine;

public class GreenOrbs : MonoBehaviour
{
    [SerializeField] private int healAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.Heal(healAmount);

            Destroy(gameObject);
        }
    }
}