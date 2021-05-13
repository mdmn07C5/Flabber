using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventGameState : UnityEvent<GameState.state, GameState.state> { }
    [System.Serializable] public class EventInt : UnityEvent<int> { }
    [System.Serializable] public class EventVoid : UnityEvent { }
}