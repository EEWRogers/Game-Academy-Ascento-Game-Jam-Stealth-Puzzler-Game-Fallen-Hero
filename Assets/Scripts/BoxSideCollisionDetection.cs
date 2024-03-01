using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSideCollisionDetection : MonoBehaviour
{
    [SerializeField] ColliderSide colliderSide;
    public ColliderSide ColliderSide { get { return colliderSide; } }
    bool isBeingPushed = false;
    public bool IsBeingPushed { get { return isBeingPushed; } }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isBeingPushed = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isBeingPushed = false;
        }
    }
}
