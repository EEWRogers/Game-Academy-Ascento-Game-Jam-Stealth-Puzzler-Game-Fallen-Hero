using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    [SerializeField] float timeBeforePushing = 1f;
    [SerializeField] float boxSpeed = 10f;
    float pushTimer;
    bool beingPushed;
    ColliderSide sideBeingPushed = ColliderSide.NONE;

    void Update() 
    {
        CheckWhichSideBeingPushed();
        MoveBox();
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

    void MoveBox()
    {
        if (sideBeingPushed != ColliderSide.NONE)
        {
            Vector3 originalPosition = transform.position;

            if (!beingPushed)
            {
                pushTimer = timeBeforePushing;
                beingPushed = true;
            }

            pushTimer -= Time.deltaTime;
            Debug.Log(pushTimer);

            if (pushTimer <= Mathf.Epsilon)
            {
                Vector3 direction = Vector3.zero;

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
                }

                transform.Translate(direction * Time.deltaTime * boxSpeed);

                if (Vector3.Distance(transform.position, originalPosition + direction) >= 1.0f)
                {
                    transform.position = originalPosition + direction;
                    beingPushed = false;
                }
            }
        }
    }
}
