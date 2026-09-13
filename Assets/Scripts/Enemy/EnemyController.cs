using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }

    public void PlayDeath()
    {
        Debug.Log(animator.name);
        Debug.Log(animator.runtimeAnimatorController.name);

        animator.SetTrigger("Death");
    }


}