﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] static GameManager instance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject pipeSpawner;
    [SerializeField] private GameState.state currentState;
    
    public static Events.EventVoid OnEscapeKeyPressed = new Events.EventVoid();
    public static Events.EventVoid OnJumpKeyPressed = new Events.EventVoid();
    public static Events.EventGameState OnGameStateChanged = new Events.EventGameState();

    

#region Unity Callbacks
    void Awake()
    {
        if (null != instance && this != instance)
        {
            Destroy(gameObject);
        }
        else
        {   
            instance = this;    
        }
        Kill.OnPlayerCollision.AddListener(HandlePlayerCollision);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.state.PREGAME;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.state.PLAYING && Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            OnEscapeKeyPressed.Invoke();
        }
    }
#endregion

    public void StartGame()
    {
        UpdateState(GameState.state.PLAYING);
        pipeSpawner.SetActive(true);
        player.SetActive(true);
        Time.timeScale = 1.0f;
    }

    private void TogglePause()
    {
        if (currentState == GameState.state.POSTGAME || currentState == GameState.state.PREGAME ) return;
        GameState.state nextState = currentState == GameState.state.PLAYING ? GameState.state.PAUSED : GameState.state.PLAYING;
        float timescale = currentState == GameState.state.PLAYING ? 0.0f : 1.0f;
        UpdateState(nextState);
        Time.timeScale = timescale; 
    }

    private void UpdateState(GameState.state nextState)
    {
        GameState.state prevState = currentState;
        OnGameStateChanged.Invoke(prevState, nextState);
        currentState = nextState;
    }

    private void HandlePlayerCollision()
    {
        UpdateState(GameState.state.POSTGAME);
        pipeSpawner.SetActive(false);
        player.SetActive(false);
        Time.timeScale = 0.0f;
    }

}

