using TMPro;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text tutorialText;

    [Header("Texto")]
    [TextArea(3, 8)]
    [SerializeField] private string message;

    private bool shown;

    private void OnTriggerEnter(Collider other)
    {
        if (shown)
            return;

        if (!other.CompareTag("Player"))
            return;

        tutorialText.text = message;
        tutorialPanel.SetActive(true);

        shown = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        tutorialPanel.SetActive(false);
    }
}