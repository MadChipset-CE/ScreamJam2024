using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera playerView;
    private List<GhostBehaviour> existingGhosts = new List<GhostBehaviour>();

    public void addNewGhost(GhostBehaviour newGhost) {
        existingGhosts.Add(newGhost);
    }

    public void destroyGhost(GhostBehaviour ghost) {
        existingGhosts.Remove(ghost);
    }

    public List<GhostBehaviour> getVisibleGhosts() {
        List<Transform> visibleTransforms =  MadUtils.getVisibleTransforms(playerView, existingGhosts.Select(ghost => ghost.transform).ToList(), 0.85f);

        return visibleTransforms
                    .Select(transform => transform.GetComponent<GhostBehaviour>()) 
                    .Where(ghost => ghost != null)  
                    .ToList();
    }
}
