using UnityEngine;

public class RedOrbPickup : MonoBehaviour
{
    [SerializeField] private int amount = 25;
    [SerializeField] private float startSpeed = 3f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 20f;

    private float currentSpeed;

    private RedOrbs playerRedOrbs;
    private Transform player;

    private bool followPlayer;

    void Update()
    {
        if (!followPlayer)
            return;

        transform.position = Vector3.MoveTowards(
    transform.position,
    player.position,
    currentSpeed * Time.deltaTime);
    }

   
    private void LateUpdate()
    {
        if (!followPlayer)
            return;

        if (Vector3.Distance(transform.position, player.position) < 0.3f)
        {
            playerRedOrbs.AddRedOrbs(amount);

            Destroy(gameObject);
        }
    }
    public void StartFollowing(Transform playerTransform)
    {
        player = playerTransform;

        playerRedOrbs = player.GetComponent<RedOrbs>();

        followPlayer = true;

        currentSpeed = startSpeed;

        GetComponent<OrbFloat>().enabled = false;
        GetComponent<OrbRotate>().enabled = false;
    }
}