using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] GameObject StartPoint;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] private FinishPoint finishPoint;
    [SerializeField] private Stage[] stages;
    [SerializeField] public Transform TF;
    [SerializeField] public Transform StartPointTF;
    private Player player;
    private Rigidbody rbPlayer;
    public bool isWin;
    private CameraFollow cameraFollow;
    private List<EColorType> listColor = new List<EColorType>();
    private List<Vector3> listPoint = new List<Vector3>();

    public int numberEnemy;
    private List<Enemy> listEnemy = new List<Enemy>();

    void Awake() // fix late
    {
        player = LevelManager.Instance.player;
        rbPlayer = player.gameObject.GetComponent<Rigidbody>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        TF = gameObject.transform;
        StartPointTF = StartPoint.transform;

    }

    //Ham khoi tao cac Object trong 1 level: Stages, GenBrick, OnInit Player
    public void OnInit()
    {
        this.Despawn();
        foreach (Stage stage in stages)
        {
            stage.OnInit();
        }
        stages[0].SpawnAllBrick(numberEnemy);
        GetListColor();
        GenListPoint();
        GenCharacter();
    }

    public void OnStart()
    {
        foreach (Enemy enemy in listEnemy)
        {
            enemy.OnStart();
        }
        player.currentLevel = this;
        finishPoint.currentLevel = this;
    }

    public void Despawn()
    {
        foreach (Stage stage in stages)
        {
            stage.OnDespawn();
        }
        foreach (Enemy enemy in listEnemy)
        {
            enemy.ClearCharBrick();
            enemy.OnDespawn();
        }
        player.ClearCharBrick();
        listColor.Clear();
        listPoint.Clear();
    }
    private void GetListColor()
    {
        for (int i = 0; i < numberEnemy + 1; i++)
        {
            listColor.Add((EColorType)i);
        }
    }

    private void GenListPoint()
    {
        float wGround = StartPointTF.localScale.x;
        Vector3 position = StartPointTF.position;
        for (int i = 0; i < numberEnemy + 1; i++)
        {
            position.x = StartPointTF.position.x - wGround / 2 + (wGround / numberEnemy) * (i) + 2f;
            position.y = StartPointTF.position.y + 1.5f;
            listPoint.Add(position);
        }
    }

    private void GenCharacter()
    {
        int index = Random.Range(0, numberEnemy + 1);
        player.SetColor(listColor[index]);
        player.TF.position = listPoint[index];
        player.OnInit();
        rbPlayer.isKinematic = false;
        listColor.RemoveAt(index);
        listPoint.RemoveAt(index);
        for (int i = 0; i < listColor.Count; i++)
        {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, listPoint[i], Quaternion.identity);
            listEnemy.Add(enemy);
            enemy.OnInit();
            listEnemy.Add(enemy);
            enemy.SetColor(listColor[i]);
        }
    }


}
