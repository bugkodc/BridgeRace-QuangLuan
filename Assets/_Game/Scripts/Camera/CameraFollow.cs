using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform transformCamera;
    public Player player;
    public Vector3 offset;
    public float lerpRate;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        transformCamera= gameObject.transform;
    }
    private void FixedUpdate()
    {
        if(!GameManagerr.Instance.IsState(EGameState.Finish))
        {
            Follow();
        }
    }
    //TODO: Camere Follow player
    private void Follow()
    {
        Vector3 pos = transformCamera.position;
        Vector3 targetPos = player.TF.position + offset;
        pos = Vector3.Lerp(pos, targetPos, lerpRate * Time.deltaTime);
        transformCamera.position = pos;
    }
    //TODO: Camere khi EndGame
    public void FollowEndGame(Vector3 position)
    {
        Vector3 pos = transformCamera.position;
        Vector3 targetPos = position + offset;
        transformCamera.position = targetPos;
    }
}
