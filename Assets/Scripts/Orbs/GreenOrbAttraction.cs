using UnityEngine;

public class GreenOrbAttraction : MonoBehaviour
{
    private GreenOrbPickup pickup;

    private void Start()
    {
        pickup = GetComponentInParent<GreenOrbPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.StartFollowing(other.transform);
        }
    }
}