using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    [SerializeField] private Item consumableItem;
    [SerializeField] private Item keyItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.AddItem(consumableItem);

            Debug.Log("Consumível adicionado");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.AddItem(keyItem);

            Debug.Log("Item-chave adicionado");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.RemoveItem(consumableItem);

            Debug.Log("Consumível removido");
        }
    }
}