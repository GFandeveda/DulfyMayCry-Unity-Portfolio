using TMPro;
using UnityEngine;

public class RedOrbsUI : MonoBehaviour
{
    [SerializeField] private RedOrbs redOrbs;
    [SerializeField] private TMP_Text redOrbsText;

    void Update()
    {
        redOrbsText.text = "Red Orbs: " + redOrbs.GetRedOrbs();
    }
}