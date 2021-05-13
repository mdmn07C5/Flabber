using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public static Events.EventInt OnPlayerClear = new Events.EventInt();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnPlayerClear.Invoke(1);
        }
    }
}
