using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] LayerMask whatToLookFor;
    Vector3 direction;
    Vector3 destination;
    bool beingPushed = false;
    ColliderSide sideBeingPushed = ColliderSide.NONE;

    void Update() 
    {
        CheckWhichSideBeingPushed();
        SetDirectionOfPush();

        if (CheckMovingDirectionNotBlocked(direction))
        {
            MoveToDestination();
        }

        Debug.Log(direction);
    }

    void CheckWhichSideBeingPushed()
    {
        sideBeingPushed = ColliderSide.NONE;

        for (int i = 0; i < transform.childCount; i++)
        {
            BoxSideCollisionDetection currentSide = transform.GetChild(i).GetComponent<BoxSideCollisionDetection>();

            if (currentSide.IsBeingPushed)
            {
                sideBeingPushed = currentSide.ColliderSide;
            }
        }
    }

    void SetDirectionOfPush()
    {
        switch (sideBeingPushed)
        {
            case ColliderSide.TOP:
            direction = Vector3.down;
            break;

            case ColliderSide.LEFT:
            direction = Vector3.right;
            break;

            case ColliderSide.RIGHT:
            direction = Vector3.left;
            break;

            case ColliderSide.BOTTOM:
            direction = Vector3.up;
            break;

            case ColliderSide.NONE:
            direction = Vector3.zero;
            break;
        }
    }

    bool CheckMovingDirectionNotBlocked(Vector3 direction)
    {
        if (Physics2D.Raycast(transform.position, direction, 1f, whatToLookFor))
        {
            return false;
        }

        return true;

    }

    void MoveToDestination()
    {
        if (!beingPushed)
        {
            destination = transform.position + direction;
        }

        if (Vector3.Distance(transform.position, destination) < Mathf.Epsilon)
        {
            transform.position = destination;
            beingPushed = false;
        }

        else
        {
            beingPushed = true;
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }

    }
}
