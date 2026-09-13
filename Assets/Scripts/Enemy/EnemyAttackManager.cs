using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager Instance;

    [SerializeField] private int maxAttackers = 2;

    private int currentAttackers;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanAttack()
    {
        return currentAttackers < maxAttackers;
    }

    public void StartAttack()
    {
        currentAttackers++;
    }

    public void EndAttack()
    {
        currentAttackers--;

        if (currentAttackers < 0)
            currentAttackers = 0;
    }
}