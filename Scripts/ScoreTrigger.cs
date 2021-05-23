using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public static Events.EventVoid OnPlayerClear = new Events.EventVoid();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ScoreZone")
        {
            OnPlayerClear.Invoke();
        }
    }
}
