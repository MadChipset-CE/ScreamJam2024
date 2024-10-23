using Unity.VisualScripting;
using UnityEngine;

public enum ghostStates {
    idle, chassing, attacking, dying
}

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] private float viewAngle = 60f, viewDistance = 20f, distanceOfAtack = 2f;
    private GameObject player;
    private ghostStates state;
    private void Update() {
        switch(state) {
            case ghostStates.idle:
                if(MadUtils.is3DPositionInsideAngle(this.transform, player.transform.position, viewAngle) && Vector3.Distance(this.transform.position, player.transform.position) < viewDistance) {
                    
                }
                break;
        }
        
    }

    private void goToPlayer() {
        
    }
}
