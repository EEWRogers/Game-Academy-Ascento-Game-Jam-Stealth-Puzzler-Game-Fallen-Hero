using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] LayerMask whatToLookFor;
    PolygonCollider2D enemyVisionCollider;
    SpriteRenderer enemySpriteRenderer;
    

    bool playerSeen = false;

    void Awake() 
    {
        enemyVisionCollider = GetComponent<PolygonCollider2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() 
    {
        if (playerSeen)
        {
            enemySpriteRenderer.color = Color.green;
        }
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
