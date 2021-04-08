using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameBaseState GameState;

    public void TransitionState(GameBaseState newGameState)
    {
        switch (newGameState)
        {
            case GameBaseState.PREPLAY:
                break;
            case GameBaseState.PLAY:
                break;
            case GameBaseState.GAMEOVER:
                break;
        }
        GameState = newGameState;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (this != instance)
        {
            Destroy(gameObject);
        }
        TransitionState(GameBaseState.PLAY);
        this.RegisterListener(GameEvent.OnPlayerFall, (param) => OnPlayerFallHandler());
    }

    private void OnPlayerFallHandler()
    {
        TransitionState(GameBaseState.GAMEOVER);
    }
}
