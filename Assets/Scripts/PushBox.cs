using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    bool beingPushed = false;
    ColliderSide sideBeingPushed = ColliderSide.NONE;

    void Update() 
    {
        CheckWhichSideBeingPushed();
        Debug.Log(sideBeingPushed);
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

}
