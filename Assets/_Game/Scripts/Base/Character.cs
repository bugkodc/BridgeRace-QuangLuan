using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] protected Transform brickHolder;
    [SerializeField] protected ChaBrick chaBrick;
    [SerializeField] private Transform nextPoint;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Animator anim;
    private string currentAnimName;
    private Dictionary<ChaBrick, bool> charBirckPool = new Dictionary<ChaBrick, bool>();
    private Queue<ChaBrick> queueCharBirck = new Queue<ChaBrick>();
    private int numberCharBrickPool = 40;
    public bool isMove = true;
    public bool isGround = true;
    public List<ChaBrick> listBrick = new List<ChaBrick>();
    public Stage currentStage;
    public Level currentLevel;
    public bool isNewState = false;
    public bool isForward => JoystickInput.Instance._joystick.Vertical > 0;



    //Trigger with brick to add brick; with character to remove brick
    private void OnTriggerEnter(Collider other)
    {
        Brick gbrick = Cache.GetBrick(other);
        Character character = Cache.GetCharacter(other);
        CheckBrick(gbrick);
        CheckCharacter(character);
    }

    public void CheckBrick(Brick gbrick)
    {
        if (gbrick == null) return;
        isGround = true;
        if (gbrick.colorType == colorType)
        {
            AddBirck();
            gbrick.Remove();
        }
        if (currentStage != null)
        {
            if (gbrick.colorType == EColorType.Default && Mathf.Abs(gbrick.TF.position.y - currentStage.transfromStage.position.y) < 0.2)
            {
                AddBirck();
                gbrick.OnDespawnColBrick();
            }

        }

    }
    public void CheckCharacter(Character character)
    {
        if (character != null && isGround)
        {
            Character scharacter = character.listBrick.Count > this.listBrick.Count ? this : character;
            if (currentStage != null)
            {
                currentStage.SpawnBrickCollider(scharacter);
            }
        }
    }
    //Add brick into character
    public void AddBirck()
    {
        ChaBrick brick = queueCharBirck.Dequeue();
        brick.gameObject.SetActive(true);

        brick.SetColor(colorType);
        brick.TF.localPosition = Vector3.up * listBrick.Count * 0.1f;
        listBrick.Add(brick);
    }

    //Remove brick from character
    public void RemoveBrick()
    {
        if (listBrick.Count > 0)
        {
            ChaBrick brick = listBrick[listBrick.Count - 1];
            listBrick.RemoveAt(listBrick.Count - 1);
            if (brick != null)
            {
                queueCharBirck.Enqueue(brick);
                brick.gameObject.SetActive(false);

            }

        }
    }

    //Check in bridge
    public bool CheckStair()
    {
        isMove = true;
        RaycastHit hit;
        if (Physics.Raycast(nextPoint.position, Vector3.down, out hit, 1000f, stairLayer))
        {
            Stair stair = Cache.GetStair(hit.collider);
            if (stair != null)
            {
                isGround = false;
            }
            if (listBrick.Count > 0 && (stair.colorType != this.colorType))
            {
                stair.SetColor(this.colorType);
                RemoveBrick();
                currentStage.SpawnOneBrick(colorType);
            }
            if (isForward && (stair.colorType != colorType))
            {
                isMove = false;
            }
        }
        return isMove;
    }



    //Clear all brick in character
    public void ClearCharBrick()
    {
        foreach (ChaBrick brick in listBrick)
        {
            if (brick != null)
            {
                Destroy(brick.gameObject);
            }
        }
        listBrick.Clear();
    }

    //Change Anim
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void InitPool()
    {
        for (int i = 0; i < numberCharBrickPool; i++)
        {
            ChaBrick brick = Instantiate(chaBrick, brickHolder);
            queueCharBirck.Enqueue(brick);
            brick.gameObject.SetActive(false);
            charBirckPool.Add(brick, false);
        }
    }
}
