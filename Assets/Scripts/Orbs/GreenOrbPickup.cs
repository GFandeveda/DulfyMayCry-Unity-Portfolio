using UnityEngine;

public class GreenOrbPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 25;

    private PlayerHealth playerHealth;
    private Transform player;

    private bool followPlayer;

    [SerializeField] private float startSpeed = 3f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 20f;

    private float currentSpeed;
    private GreenOrbs playerGreenOrbs;
    

    void Update()
    {
        if (!followPlayer)
            return;

        currentSpeed += acceleration * Time.deltaTime;

        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            currentSpeed * Time.deltaTime);
    }


    void LateUpdate()
    {
        if (!followPlayer)
            return;

        if (Vector3.Distance(transform.position, player.position) < 0.3f)
        {
            playerHealth.Heal(healAmount);

            Destroy(gameObject);
        }
    }
    public void StartFollowing(Transform playerTransform)
    {
        player = playerTransform;

        playerHealth = player.GetComponent<PlayerHealth>();

        followPlayer = true;

        currentSpeed = startSpeed;

        GetComponent<OrbFloat>().enabled = false;
        GetComponent<OrbRotate>().enabled = false;
    }
}