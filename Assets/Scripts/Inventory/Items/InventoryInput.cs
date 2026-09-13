using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventoryUI inventoryUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool open = !inventoryPanel.activeSelf;

            inventoryPanel.SetActive(open);

            if (open)
            {
                inventoryUI.Refresh();
            }
        }
    }
}