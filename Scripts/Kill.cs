using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    // public delegate void PipeCollision();
    // public static event PipeCollision OnPlayerCollision;

    public static Events.EventVoid OnPlayerCollision = new Events.EventVoid();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnPlayerCollision.Invoke();
        }
    }
    
}
