using UnityEngine;

public enum AmmoType {
    polaroid, lighterGas
}

[CreateAssetMenu(menuName = "Mad Chipset/Item/Ammo")]
public class ItemAmmo : Item
{
    public AmmoType ammoType;

    public override Item copyData() {
        ItemAmmo item = new ItemAmmo();
        item.description = this.description;
        item.object3D = this.object3D;
        item.ammoType = this.ammoType;
        return item;
    }
}
