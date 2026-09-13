using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera")]
    public Vector3 offset = new Vector3(-2.3f, 3.3f, -5.8f);

    public float positionSmooth = 8f;
    public float rotationSmooth = 8f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Posição desejada
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            1f / positionSmooth);

        // Olhar para o peito do personagem
        Vector3 lookTarget = target.position + Vector3.up * 1.4f;

        Quaternion targetRotation =
            Quaternion.LookRotation(lookTarget - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSmooth * Time.deltaTime);
    }
}