using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float dodgeSpeed = 10f;
    [SerializeField] private float dodgeDuration = 0.4f;

   
    private float dodgeTimer;
    private Vector3 dodgeDirection;

    private PlayerCombat playerCombat;
    [Header("Movement")]

    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    private float verticalVelocity;

    private CharacterController controller;

    private Animator animator;
    private PlayerHealth playerHealth;

   
    void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (playerCombat.IsDodging)
        {
            if (dodgeTimer <= 0)
            {
                dodgeDirection = transform.forward;
                dodgeTimer = dodgeDuration;
            }

            dodgeTimer -= Time.deltaTime;

            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

            if (state.IsName("Roll") && state.normalizedTime < 0.70f)
            {
                controller.Move(dodgeDirection * dodgeSpeed * Time.deltaTime);
            }

            ApplyGravity();

            return;
        }

        if (playerHealth.IsDead)
            return;
        Move();
        ApplyGravity();
    }

    void Move()
    {
        if (playerCombat.IsBusy)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = cameraForward * vertical + cameraRight * horizontal;

        if (movement.magnitude > 1f)
            movement.Normalize();

        controller.Move(movement * moveSpeed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);

            float speedPercent = 0f;

            if (movement.magnitude > 0.1f)
            {
                speedPercent = 1f;
            }

            animator.SetFloat("Speed", speedPercent);
        }
    }
    void ApplyGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 gravityMove = new Vector3(0f, verticalVelocity, 0f);

        controller.Move(gravityMove * Time.deltaTime);
    }
}