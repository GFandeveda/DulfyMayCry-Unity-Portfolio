using UnityEngine;

public class OrbFloat : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatHeight = 0.15f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 position = startPosition;

        position.y += Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = position;
    }
}