using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    [SerializeField] GameObject objectConnectedToSwitch;
    public bool isTriggered = false;

    void Update() 
    {
        objectConnectedToSwitch.SetActive(!isTriggered);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PushableObject"))
        {
            isTriggered = true;
            Debug.Log("Box has triggered me!");
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PushableObject"))
        {
            isTriggered = false;
            Debug.Log("Box has stopped triggering me!");
        }
    }
}
