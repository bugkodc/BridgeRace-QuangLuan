using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : Singleton<Data>
{
    private int level;
    public int GetLevel()
    {
        return level;
    }
    public void SetLevel(int level)
    {

        this.level = level;
    }
    public int GetNextLevel()
    {
        level++;
        return level;
    }
}
