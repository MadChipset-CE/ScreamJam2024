using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Mad Chipset/Item/Generic")]
public class Item : ScriptableObject
{
    public string description;
    public GameObject object3D;

    public virtual Item copyData() {
        Item item = new Item();
        item.description = this.description;
        item.object3D = this.object3D;
        return item;
    }

}
