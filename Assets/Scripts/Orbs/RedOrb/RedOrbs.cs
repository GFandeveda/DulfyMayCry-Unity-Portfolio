using UnityEngine;


public class RedOrbs : MonoBehaviour
{
    [SerializeField] private int currentRedOrbs;

    public int GetRedOrbs()
    {
        return currentRedOrbs;
    }

    public void AddRedOrbs(int amount)
    {
        currentRedOrbs += amount;
    }

    public bool SpendRedOrbs(int amount)
    {
        if (currentRedOrbs >= amount)
        {
            currentRedOrbs -= amount;
            return true;
        }

        return false;
    }
}