using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Item item;

    public void Buy()
    {
        shopManager.BuyItem(item);
    }
}