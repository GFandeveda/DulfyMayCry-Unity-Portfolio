using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Circle,
        PrepareAttack,
        Attack,
        Recover
    }

    private EnemyState currentState;
    private static int enemyCount = 0;
    private int myIndex;

    private EnemyHealth enemyHealth;
    [SerializeField] private float circleRadius = 2.5f;
    [SerializeField] private int circleIndex = 0;
    private Animator animator;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float rotationSpeed = 360f;
    private EnemySwordHitbox swordHitbox;

    private float attackTimer;
    private float circleTimer;
    private float circleDuration;

    private float prepareTimer;
    private float recoverTimer;

    private Transform player;

    void Start()
    {
        currentState = EnemyState.Chase;
        circleDuration = Random.Range(0.8f, 2f);
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        swordHitbox = GetComponentInChildren<EnemySwordHitbox>();

        enemyHealth = GetComponent<EnemyHealth>();
        myIndex = enemyCount;
        enemyCount++;
    }

    void Update()
    {
        if (enemyHealth.IsDead)
        {
            swordHitbox.DisableHitbox();
            return;
        }

        attackTimer -= Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Chase:
                UpdateChase();
                break;

            case EnemyState.Circle:
                UpdateCircle();
                break;

            case EnemyState.PrepareAttack:
                UpdatePrepareAttack();
                break;

            case EnemyState.Attack:
                UpdateAttack();
                break;

            case EnemyState.Recover:
                UpdateRecover();
                break;
        }

        UpdateSwordHitbox();
    }

    void UpdateChase()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            animator.SetBool("IsWalking", true);

            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);
            }

            float angle = myIndex * (360f / enemyCount);

            Vector3 offset = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                0,
                Mathf.Sin(angle * Mathf.Deg2Rad)
            ) * circleRadius;

            Vector3 targetPosition = player.position + offset;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsWalking", false);

            circleTimer = 0;
            circleDuration = Random.Range(0.8f, 2f);

            currentState = EnemyState.Circle;
        }
    }


    void DisableSwordHitbox()
    {
        swordHitbox.DisableHitbox();
    }

    void UpdateSwordHitbox()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Attack"))
        {
            if (state.normalizedTime > 0.30f &&
                state.normalizedTime < 0.60f)
            {
                swordHitbox.EnableHitbox();
            }
            else
            {
                swordHitbox.DisableHitbox();
            }
        }
        else
        {
            swordHitbox.DisableHitbox();
        }
    }
    private void OnDestroy()
    {
        enemyCount--;
    }

    void UpdateCircle()
    {
        animator.SetBool("IsWalking", true);

        Vector3 dir = transform.position - player.position;
        dir.y = 0;

        Vector3 tangent = Vector3.Cross(Vector3.up, dir).normalized;

        transform.position += tangent * speed * 0.6f * Time.deltaTime;

        transform.LookAt(new Vector3(
            player.position.x,
            transform.position.y,
            player.position.z));

        circleTimer += Time.deltaTime;

        if (circleTimer >= circleDuration)
        {
            if (EnemyAttackManager.Instance.CanAttack())
            {
                EnemyAttackManager.Instance.StartAttack();

                circleTimer = 0;
                circleDuration = Random.Range(0.8f, 2f);

                currentState = EnemyState.PrepareAttack;
            }
        }
    }
    void UpdatePrepareAttack()
    {
        animator.SetBool("IsWalking", false);

        prepareTimer += Time.deltaTime;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        if (prepareTimer >= 0.5f)
        {
            prepareTimer = 0;

            animator.SetTrigger("Attack");
            attackTimer = attackCooldown;

            currentState = EnemyState.Attack;
        }
    }
    void UpdateAttack()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (!state.IsName("Attack"))
        {
            recoverTimer = 0;
            currentState = EnemyState.Recover;
        }
    }
    void UpdateRecover()
    {
        animator.SetBool("IsWalking", false);

        recoverTimer += Time.deltaTime;

        if (recoverTimer >= 1f)
        {
            recoverTimer = 0;

            EnemyAttackManager.Instance.EndAttack();

            currentState = EnemyState.Chase;
        }
    }
}