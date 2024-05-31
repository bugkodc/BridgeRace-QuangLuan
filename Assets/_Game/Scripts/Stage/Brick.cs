using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    public Stage stage;
    public void Remove()
    {
        stage.OnDespawn(this);
    }
    public void OnDespawnColBrick()
    {
        stage.OnDespawnColBrick(this);
    }
}
