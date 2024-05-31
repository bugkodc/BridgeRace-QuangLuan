using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerr : Singleton<GameManagerr>
{
    public EGameState currentState;

    private void Awake()
    {
        ChangeState(EGameState.MainMenu);
    }
    private void Start() {
        UIManager.Instance.OpenUI<MianMenu>();
    }
    public void ChangeState(EGameState state)
    {
        if(currentState!= state)
        {
            currentState= state;
        }
    }
    public bool IsState(EGameState state)
    {
        return currentState == state;
    }
}
