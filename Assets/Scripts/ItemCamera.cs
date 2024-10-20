using System.Collections.Generic;
using UnityEngine;

public class polaroidPhoto {
    public RenderTexture image;
    public List<GhostBehaviour> ghosts = new List<GhostBehaviour>();
}

public class ItemCamera : Item
{
    public AmmoType polaroidsLeft;
    public List<polaroidPhoto> photosTaken = new List<polaroidPhoto>();

    
    
}
