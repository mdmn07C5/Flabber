using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject preGameUI;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject playingUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject scorePanelUI;

    Queue<GameObject> activeUIs = new Queue<GameObject>();

    void Awake()
    {
        GameManager.OnGameStateChanged.AddListener(HandleOnGameStateChanged);
        activeUIs.Enqueue(preGameUI);
        activeUIs.Enqueue(startButton);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach( var UI in activeUIs )
        {
            UI.SetActive(true);
        }
    }
    
    void HandleOnGameStateChanged(GameState.state prevState, GameState.state nextState)
    {
        foreach (var UI in activeUIs )
        {
            UI.SetActive(false);
        }
        activeUIs.Clear();

        switch(nextState)
        {
            case GameState.state.PLAYING:
                activeUIs.Enqueue(playingUI);
                break;

            case GameState.state.POSTGAME:
                activeUIs.Enqueue(gameOverUI);
                activeUIs.Enqueue(startButton);
                activeUIs.Enqueue(scorePanelUI);
                break;

            case GameState.state.PAUSED:
                activeUIs.Enqueue(pauseUI);
                activeUIs.Enqueue(scorePanelUI);
                break;

            default:
                break;

        }

        foreach (var UI in activeUIs)
        {
            UI.SetActive(true);
        }
    }
}
