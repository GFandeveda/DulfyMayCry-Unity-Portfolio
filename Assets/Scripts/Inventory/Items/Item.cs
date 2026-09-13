using UnityEngine;

public enum ItemType
{
    Consumable,
    KeyItem
}

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    [TextArea]
    public string description;

    public int price;
    public ItemType itemType;
}