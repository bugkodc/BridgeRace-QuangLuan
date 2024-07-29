using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBridgeState : IState<Enemy>
{
    public void OnEnter(Enemy enemy)
    {
        enemy.MoveToBrigde();
    }
    public void OnExecute(Enemy enemy)
    {
        if (enemy.numberBrick == 0 || enemy.isNewState)
        {
            enemy.ChangeState(new CollectState());
        }
        else
        {
            enemy.MoveToBrigde();
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
