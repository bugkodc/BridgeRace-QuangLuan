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

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!GameManagerr.Instance.IsState(EGameState.Finish))
        {
            Follow();
        }
    }
    private void Follow()
    {
        Vector3 pos = transformCamera.position;
        Vector3 targetPos = player.TF.position + offset;
        pos = Vector3.Lerp(pos, targetPos, lerpRate*Time.deltaTime);
        transformCamera.position = pos;
    }
    public void FollowEndGame(Vector3 position)
    {
        Vector3 pos = transformCamera.position;
        Vector3 targetPos = position + offset;
        transformCamera.position = targetPos;
    }
}
