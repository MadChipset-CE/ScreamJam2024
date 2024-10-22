using System.Collections.Generic;
using UnityEngine;

public class polaroidPhoto {
    public RenderTexture image;
    public List<GhostBehaviour> ghosts = new List<GhostBehaviour>();
}

[CreateAssetMenu(menuName = "Mad Chipset/Item/Camera")]
public class ItemCamera : Item
{
    public AmmoType ammoType;
    public List<polaroidPhoto> photosTaken = new List<polaroidPhoto>();


    public override Item copyData() {
        ItemCamera item = new ItemCamera();
        item.description = this.description;
        item.object3D = this.object3D;
        item.ammoType = this.ammoType;
        item.photosTaken = this.photosTaken;
        return item;
    }
}
