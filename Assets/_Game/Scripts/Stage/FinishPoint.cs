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
        transformFinishPoint= gameObject.transform;
    }

    private void Start()
    {
        cameraFollow= FindObjectOfType<CameraFollow>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(currentLevel!=null)
        {
            cameraFollow.FollowEndGame(transformFinishPoint.position);
            Character character = other.GetComponent<Character>();
            if(character != null)
            {
                character.ClearCharBrick();
                character.ChangeAnim(Constant.ANIM_WIN);
                Type playerType = (new Player()).GetType();
                Type characterType = character.GetType();
                if(characterType.IsAssignableFrom(playerType))
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
    }
}
