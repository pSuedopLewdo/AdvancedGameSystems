using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemId, itemDesc;
    [SerializeField] public string itemName;
    [SerializeField] private int stackMax, stackCurrent, cost;
    [SerializeField] private bool stackable;

    [SerializeField] private ItemType itemType;
    [SerializeField] public Sprite icon;

}

public enum ItemType
{
    Flower,
    Clothes,
    Consumable,
    Materials,
    Quest,
    Miscellaneous,
}
