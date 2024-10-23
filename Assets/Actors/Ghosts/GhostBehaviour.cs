using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum ghostStates {
    idle, chassing, attacking, dying
}

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] private float viewAngle = 60f, viewDistance = 20f, distanceOfAtack = 2f;
    private GameObject player;
    private ghostStates state = ghostStates.idle;
    private int life = 4;
    private NavMeshAgent agent;


    private void Awake() {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    float elapsedTimeWitoutTarget = 0;
    private void Update() {
        switch(state) {
            case ghostStates.idle:
                if(MadUtils.is3DPositionInsideAngle(this.transform, player.transform.position, viewAngle) && Vector3.Distance(this.transform.position, player.transform.position) < viewDistance) {
                    state = ghostStates.chassing;
                }
                break;
            case ghostStates.chassing:
                agent.destination = player.transform.position;
                agent.stoppingDistance = distanceOfAtack;
                if(Vector3.Distance(this.transform.position, player.transform.position) < distanceOfAtack) {
                    state = ghostStates.attacking;
                }
                break;
            case ghostStates.attacking:
                this.transform.LookAt(player.transform, Vector3.up);
                if(Vector3.Distance(this.transform.position, player.transform.position) > distanceOfAtack * 2) {
                    // atack
                }
                break;
        }

        if(state != ghostStates.idle && (!MadUtils.is3DPositionInsideAngle(this.transform, player.transform.position, viewAngle) || Vector3.Distance(this.transform.position, player.transform.position) > viewDistance)) { 
            elapsedTimeWitoutTarget += Time.deltaTime;
            if(elapsedTimeWitoutTarget > 10f) {
                state = ghostStates.idle;
                elapsedTimeWitoutTarget = 0;
            }
        } else {
            elapsedTimeWitoutTarget = 0;
        }
        
    }
}
