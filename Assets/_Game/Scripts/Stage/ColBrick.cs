using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBrick : Brick
{
    [SerializeField] public Rigidbody rgColBrick;
    [SerializeField] public Collider colliderColBrick;
   private void Start() 
   {
     rgColBrick = GetComponent<Rigidbody>();
     colliderColBrick = GetComponent<Collider>();
     SetColor(EColorType.Default);
     Vector3[] directions = new Vector3[]{Vector3.right, Vector3.left, Vector3.forward, Vector3.back};
     int random = Random.Range(0, 4);
     rgColBrick.AddForce(Vector3.up*100 + directions[random]*200);
   }
}
