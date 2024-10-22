using UnityEngine;

public class ItemObject : Interactive
{
    [SerializeField] private Item item;

    public Item getItem() {
        return this.item;
    }
}
