using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] LayerMask whatToLookFor;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 0.2f;
    [SerializeField] float guardSpeed = 1.2f;
    [SerializeField] float rotationSpeed = 10f;
    PolygonCollider2D enemyVisionCollider;
    SpriteRenderer enemySpriteRenderer;
    Transform enemyCentreTransform;
    
    bool playerSeen = false;
    int currentWaypointIndex = 0;

    void Awake() 
    {
        enemyVisionCollider = GetComponentInChildren<PolygonCollider2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyCentreTransform = transform.GetChild(0).transform;
    }

    void Update() 
    {
        if (playerSeen)
        {
            enemySpriteRenderer.color = Color.green;
        }

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                GetNextWaypoint();
            }
            MoveToLocation(GetCurrentWaypoint());
        }

    }

    Vector2 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypointPosition(currentWaypointIndex);
    }

    void GetNextWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextWaypoint(currentWaypointIndex);
    }

    bool AtWaypoint()
    {
        float distanceToWaypoint = Vector2.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    void MoveToLocation(Vector3 destination)
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, guardSpeed * Time.deltaTime);
        RotateViewTowardsDestination(destination);
    }

    void RotateViewTowardsDestination(Vector3 destination)
    {
        Vector3 vectorToTarget = destination - enemyCentreTransform.position;

        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Something entered my vision");

        if (enemyVisionCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("The player entered my vision");
            Vector2 directionOfOther = other.transform.position - transform.position;
            RaycastHit2D objectHit = Physics2D.Raycast(transform.position, directionOfOther, Mathf.Infinity, whatToLookFor);

            if (objectHit.collider != null)
            {
                Debug.Log("Raycast hit something: " + objectHit.collider.name);

                if (objectHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    playerSeen = true;
                }

                else
                {
                    Debug.Log("Whatever entered my sight was not the player");
                }
            }
        }
    }
}
