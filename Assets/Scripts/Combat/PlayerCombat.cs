using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] swingClips;

    [SerializeField] private KeyCode dodgeKey = KeyCode.Space;

    private bool isDodging;

    public bool IsDodging => isDodging;
    public bool IsBusy =>
    isBlocking ||
    animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
    animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
    animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3") ||
    animator.GetCurrentAnimatorStateInfo(0).IsName("StrongAttack") ||
    animator.GetCurrentAnimatorStateInfo(0).IsName("SpinAttack");

    [SerializeField] private KeyCode blockKey = KeyCode.Mouse1;

    private bool isBlocking;

    public bool IsBlocking => isBlocking;
    private bool isStrongAttack;
    [SerializeField] private KeyCode strongAttackKey = KeyCode.Q;
    [SerializeField] private KeyCode spinAttackKey = KeyCode.E;

    private PlayerHealth playerHealth;
    private bool hitboxEnabled = false;

    [SerializeField]
    private float hitboxDuration = 0.3f;

    private float hitboxTimer = 0f;

    private SwordHitbox swordHitbox;
    private Animator animator;

    private int comboStep = 0;

    private float comboTimer = 0f;

    [SerializeField]
    private float comboTime = 0.8f;

    private bool isDead;
    public bool IsDead => isDead;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        swordHitbox = GetComponentInChildren<SwordHitbox>();

        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth.IsDead)
            return;
        UpdateDodge();
        HandleDodge();
        HandleBlock();
        HandleAttackInput();
        HandleStrongAttack();
        HandleSpinAttackInput();

        UpdateHitbox();

        if (comboStep > 0)
        {
            comboTimer -= Time.deltaTime;

            if (comboTimer <= 0)
            {
                ResetCombo();
            }
        }
    }
    void HandleAttackInput()
    {
        if (isBlocking)
            return;
        if (!Input.GetMouseButtonDown(0) &&
     !Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            return;
        }
      
        comboStep++;

        if (comboStep > 3)
            comboStep = 1;

        animator.SetInteger("ComboStep", comboStep);
        animator.SetTrigger("Attack");
        

        comboTimer = comboTime;
    }
    void UpdateHitbox()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // Combo normal
        if (state.IsName("Attack1") ||
            state.IsName("Attack2") ||
            state.IsName("Attack3"))
        {
            swordHitbox.SetDamage(25);

            if (state.normalizedTime > 0.25f &&
                state.normalizedTime < 0.55f)
            {
                swordHitbox.EnableHitbox();
                swordHitbox.ResetHitbox();
            }
            else
            {
                swordHitbox.DisableHitbox();
            }

            return;
        }

        // Ataque forte
        if (state.IsName("StrongAttack"))
        {
            swordHitbox.SetDamage(50);

            if (state.normalizedTime > 0.30f &&
                state.normalizedTime < 0.70f)
            {
                swordHitbox.EnableHitbox();
                swordHitbox.ResetHitbox();
            }
            else
            {
                swordHitbox.DisableHitbox();
            }

            return;
        }

        // Ataque giratório
        if (state.IsName("SpinAttack"))
        {
            swordHitbox.SetDamage(35);

            if (state.normalizedTime > 0.20f &&
                state.normalizedTime < 0.90f)
            {
                swordHitbox.EnableHitbox();
                swordHitbox.ResetHitbox();
            }
            else
            {
                swordHitbox.DisableHitbox();
            }

            return;
        }

        swordHitbox.DisableHitbox();
    }
    void ResetCombo()
    {
        comboStep = 0;

        animator.SetInteger("ComboStep", 0);
    }

    void HandleStrongAttack()
    {
        if (isBlocking)
            return;
        if (!Input.GetKeyDown(strongAttackKey) &&
            !Input.GetKeyDown(KeyCode.JoystickButton3))
            return;
        

        animator.SetTrigger("StrongAttack");
    }

    void HandleSpinAttackInput()
    {
        if (isBlocking)
            return;
        if (!Input.GetKeyDown(spinAttackKey) &&
            !Input.GetKeyDown(KeyCode.JoystickButton5))
            return;
        

        animator.SetTrigger("SpinAttack");
    }
    void UpdateAttackDamage()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("StrongAttack"))
        {
            swordHitbox.SetDamage(50);
        }
        else
        {
            swordHitbox.SetDamage(25);
            isStrongAttack = false;
        }
    }

    void HandleBlock()
    {
        bool block =
            Input.GetKey(blockKey) ||
            Input.GetKey(KeyCode.JoystickButton4); // LB

        isBlocking = block;

        animator.SetBool("IsBlocking", block);
    }

    void HandleDodge()
    {
        if (!Input.GetKeyDown(dodgeKey) &&
            !Input.GetKeyDown(KeyCode.JoystickButton0))
            return;

        if (isBlocking)
            return;

        if (playerHealth.IsDead)
            return;

        isDodging = true;

        animator.SetTrigger("Roll");
    }

    void UpdateDodge()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        isDodging = state.IsName("Roll");
    }
}