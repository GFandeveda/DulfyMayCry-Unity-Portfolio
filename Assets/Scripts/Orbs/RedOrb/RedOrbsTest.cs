using UnityEngine;

public class RedOrbsTest : MonoBehaviour
{
    [SerializeField] private RedOrbs redOrbs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {

            redOrbs.AddRedOrbs(100);
            Debug.Log(redOrbs.GetRedOrbs());
        }
    }
}