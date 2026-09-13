using UnityEngine;
using UnityEngine.EventSystems;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private Transform itemList;
    [SerializeField] private GameObject slotPrefab;

    public void Refresh()
    {
        foreach (Transform child in itemList)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemStack stack in inventory.GetItems())
        {
            GameObject slot = Instantiate(slotPrefab, itemList);

            slot.GetComponent<InventorySlotUI>()
                .Setup(stack, inventory, playerHealth);
        }
        if (itemList.childCount > 0)
        {
            EventSystem.current.SetSelectedGameObject(itemList.GetChild(0).gameObject);
        }
    }
}