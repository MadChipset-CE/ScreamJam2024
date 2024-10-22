using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform itemPositionRef;
    public Item equipedItem;
    public List<Item> itemList = new List<Item>();

    public void equipitem(Item item) {
        cleanItem();
        equipedItem = item;
        Instantiate(equipedItem.object3D, itemPositionRef);
    }

    private void cleanItem() {
        foreach(GameObject obj in itemPositionRef) {
            Destroy(obj);
        }
    }

}
