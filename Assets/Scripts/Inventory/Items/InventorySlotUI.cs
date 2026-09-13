using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text amount;

    private ItemStack stack;
    private Inventory inventory;
    private PlayerHealth playerHealth;

    public void Setup(ItemStack stack, Inventory inventory, PlayerHealth playerHealth)
    {
        this.stack = stack;
        this.inventory = inventory;
        this.playerHealth = playerHealth;

        icon.sprite = stack.item.icon;
        itemName.text = stack.item.itemName;
        amount.text = "x" + stack.amount;

        Button button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(UseItem);

        // Só permite clicar se houver pelo menos 1 item
        button.interactable = stack.amount > 0;
    }

    private void UseItem()
    {
        inventory.UseItem(stack.item, playerHealth);
    }
}