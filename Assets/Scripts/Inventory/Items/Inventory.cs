using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int maxSlots = 20;

    private List<ItemStack> items = new List<ItemStack>();

    public bool AddItem(Item item)
    {
        foreach (ItemStack stack in items)
        {
            if (stack.item == item)
            {
                stack.amount++;

                if (inventoryUI != null && inventoryUI.gameObject.activeInHierarchy)
                    inventoryUI.Refresh();

                return true;
            }
        }

        if (items.Count >= maxSlots)
            return false;

        items.Add(new ItemStack(item, 1));

        if (inventoryUI != null && inventoryUI.gameObject.activeInHierarchy)
            inventoryUI.Refresh();

        return true;
    }

    public bool RemoveItem(Item item)
    {
        foreach (ItemStack stack in items)
        {
            if (stack.item == item)
            {
                if (stack.amount <= 0)
                    return false;

                stack.amount--;

                if (inventoryUI != null && inventoryUI.gameObject.activeInHierarchy)
                {
                    inventoryUI.Refresh();
                }

                return true;
            }
        }

        return false;
    }
    public List<ItemStack> GetItems()
    {
        return items;
    }

    public void UseItem(Item item, PlayerHealth playerHealth)
    {
        ItemStack stack = items.Find(s => s.item == item);

        if (stack == null)
            return;

        if (stack.amount <= 0)
            return;

        // Só consumíveis podem ser usados
        if (item is ConsumableItem consumable)
        {
            playerHealth.Heal(consumable.healAmount);

            RemoveItem(item);
        }
    }
}