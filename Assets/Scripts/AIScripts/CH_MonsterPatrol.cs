using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CH_MonsterPatrol : MonoBehaviour {

    NavMeshAgent player;
    void Start()
    {       
        //Hittar navmesh och gör så att den börjar gå mot sin första waypoint
        player = GetComponent<NavMeshAgent>();
        player.SetDestination(wayPoints[0].position);
        playerPos = FindObjectOfType<CH_PlayerMovement>().transform;
    }
    [Header("List of Waypoints")]
    [SerializeField] Transform[] wayPoints;

    [Header("Player Transform")]
    Transform playerPos;
    [SerializeField] Transform rayOrigin;

    [Header("Vision Settings")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask hideLayer;
    [SerializeField] int visionDegrees;
    [SerializeField] int visionLenght;

    int counter;
    float newTime;
    bool gone;
    Vector3 lastPos;
    void Update()
    {           
        //Här sätter jag värden för att kunna mäta *distance* mellan objekt utan att behöva ta hänsyn till Y-värdet
        Vector3 wayPointPos = new Vector3(wayPoints[counter].position.x, 0, wayPoints[counter].position.z);
        Vector3 transformPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 currentWaypoint = new Vector3(player.destination.x, 0, player.destination.z);
        //Gör så att monstret går till en ny position när den kommit fram
        if (Vector3.Distance(wayPointPos, transformPos) < 0.2f)
        {
            counter++;
            if (counter > wayPoints.Length - 1)
            {
                counter = 0;
            }
            player.SetDestination(wayPoints[counter].position);
        }
        RaycastHit hit;
        rayOrigin.LookAt(playerPos.position, Vector3.up);
        //Detta gör så att monstret kan se spelaren om den är inom en specifik vinkel
        if (rayOrigin.localEulerAngles.y >= 0 && rayOrigin.localEulerAngles.y <= 0 + visionDegrees/2 ||  
            rayOrigin.localEulerAngles.y <= 360 && rayOrigin.localEulerAngles.y >= 360 - visionDegrees/2)
        {
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, visionLenght/*, hideLayer + playerLayer*/))
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.forward * visionLenght, Color.yellow);
                if(Vector3.Distance(transformPos, currentWaypoint) < 1.5f)
                {
                    if (!gone)
                    {
                        gone = true;
                        StartCoroutine(WalkBackTime(1.0f));
                    }
                }
                //Kollar så den ser spelaren och uptaderar positionen av spelaren med en cooldown för att vara mer optimerat
                else if (hit.collider.gameObject.tag == "Player" && Time.time > newTime)
                {
                    player.SetDestination(playerPos.position);
                    newTime = Time.time + 0.3f;
                }
                else if (Vector3.Distance(currentWaypoint, transformPos) < 0.3f && hit.collider.gameObject.tag != "Player")
                {
                    if (!gone)
                    {
                        gone = true;
                        StartCoroutine(WalkBackTime(1.0f));
                    }
                }
            }
            //Ifall monstret inte ser spelaren så går den tillbaka till waypointen den var påväg mot
            else if (Vector3.Distance(currentWaypoint, transformPos) < 0.1f)
            {
                player.SetDestination(wayPoints[counter].position);
            }
        }
        else if (Vector3.Distance(currentWaypoint, transformPos) < 0.1f)
        {
            player.SetDestination(wayPoints[counter].position);
        }
    }
    //Gör så att monstret väntar en stund innan den går tillbaka till sin vanliga bana om spelaren gömmer sig
    IEnumerator WalkBackTime(float timeToWait)
    {
        player.SetDestination(transform.position);
        yield return new WaitForSeconds(timeToWait);
        player.SetDestination(wayPoints[counter].position);
        gone = false;
    }
    
    public void NewPosition(Vector3 newPos)
    {
        player.SetDestination(newPos);
    }
}
