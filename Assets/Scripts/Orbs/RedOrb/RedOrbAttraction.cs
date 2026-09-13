using UnityEngine;

public class RedOrbAttraction : MonoBehaviour
{
    private RedOrbPickup pickup;

    private void Start()
    {
        pickup = GetComponentInParent<RedOrbPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.StartFollowing(other.transform);
        }
    }
}