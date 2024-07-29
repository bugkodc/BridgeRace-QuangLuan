using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] public Transform transformFinishPoint;
    public Level currentLevel;
    public CameraFollow cameraFollow;
    private void Awake()
    {
        transformFinishPoint = gameObject.transform;
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();// fix late
    }
    private void OnTriggerEnter(Collider other)
    {
        if (currentLevel == null) return;
        cameraFollow.FollowEndGame(transformFinishPoint.position);
        Character character = other.GetComponent<Character>();
        CheckCharacter(character);
    }
    public void CheckCharacter(Character character)
    {
        if (character == null) return;
        character.ClearCharBrick();
        character.ChangeAnim(Constant.ANIM_WIN);
        Type playerType = (new Player()).GetType();
        Type characterType = character.GetType();
        if (characterType.IsAssignableFrom(playerType)) // ep kieu character => player
        {
            currentLevel.isWin = true;

        }
        else
        {
            currentLevel.isWin = false;
        }
        LevelManager.Instance.OnFinish();
    }
}
