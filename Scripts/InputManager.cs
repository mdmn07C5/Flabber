using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Events.EventVoid OnJumpKeyPressed = new Events.EventVoid();
    public static Events.EventVoid OnEscapeKeyPressed = new Events.EventVoid();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapeKeyPressed.Invoke();
        }
    }
}
