using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [SerializeField] private Rigidbody rbPlayer;

    private void Start() 
    {
        OnInit();
    }
   
    private void Update()
    {
        if(currentLevel!= null)
        {
            if(!CheckStair() || (!JoystickInput.Instance.isControl && !GameManagerr.Instance.IsState(EGameState.Finish))) //Len cau gach khong cung mau, (khong di chuyen va game chua finish)
            {
                rbPlayer.velocity= Vector3.zero;
                ChangeAnim(Constant.ANIM_IDLE);
            }
            else if(GameManagerr.Instance.IsState(EGameState.Finish))
            {
                ChangeAnim(Constant.ANIM_WIN);
                rbPlayer.velocity= Vector3.zero;
            }
            else
            {
                JoystickInput.Instance.Move();
                ChangeAnim(Constant.ANIM_RUN);
            }
        }
    }
    public void OnInit() 
    {
        ChangeAnim(Constant.ANIM_IDLE);
        rbPlayer.velocity= Vector3.down*100;
        InitPool(); 
    }
}
