using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject goCells;
    public GameObject goGround;
    public GameObject goWay;
    public GameObject[] goTowers;
    public GameObject goStart;
    public GameObject goFinish;
    public GameObject goMonster;

    private int mapMatrixX = GlobalData.mapMatrixX;
    private int mapMatrixZ = GlobalData.mapMatrixZ;
    private int level = 1;
    private int tmpMonsterNum = 1;
    private float diffX;
    private float diffZ;
    private float tBW = GlobalData.timeBetweenWaves;
    private float spawnTime = 3;
    private float tmpTime = 0;
    private char[,] mapTmp = GlobalData.mapMatrix;
    private Vector3 startPos;
    private Vector3 finishPos;
    private GameObject goTmp;
    struct Pair
    {
        int a;
        int b;
    }
    private bool checkBorder(int x, int z)
    {
        return (x>=0 && x<mapMatrixX && z>=0 && z<mapMatrixZ);
    }
    private Vector3 getCoordinates(int x, int z)
    {
        return new Vector3(x - diffX, 0, z - diffZ);
    }
    private void generateGameField()
    {
        for(int i=0; i<mapMatrixX; i++)
        {
            for(int j=0; j<mapMatrixZ; j++)
            {
                switch(GlobalData.mapMatrix[i,j])
                {
                    case 'G': goTmp = Instantiate(goGround);
                    break;
                    case 'W': goTmp = Instantiate(goWay);
                    break;
                    case 'T': goTmp = Instantiate(goTowers[Random.Range(0, goTowers.Length)]);
                    break;
                    case 'S': goTmp = Instantiate(goStart); startPos = getCoordinates(i,j); GlobalData.startX = i; GlobalData.startZ = j;
                    break;
                    case 'F': goTmp = Instantiate(goFinish); finishPos = getCoordinates(i,j); GlobalData.finishX = i; GlobalData.finishZ = j;
                    break;
                }
                goTmp.transform.parent = goCells.transform;
                goTmp.transform.localPosition = getCoordinates(i,j);
            }
        }
    }
    private void towerDestroyed()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }
    private void waiting()
    {
        if(tmpTime<0)
        {
            tmpTime  = tBW;
            GlobalData.timeMode = 'S';                                 ///// change time mode to spawning time
        }
        else
        {
            tmpTime-=Time.deltaTime;
        }
    }
    private void spawning()
    {
        if(GlobalData.toSpawn)
        {
            Debug.Log(tmpMonsterNum);
            if(tmpMonsterNum > GlobalData.monsterCountPerWave)
            {
                tmpMonsterNum = 1;
                GlobalData.timeMode = 'P';                                 ///// change time mode to game time
            }
            else
            {
                goTmp = Instantiate(goMonster);
                goTmp.transform.parent = goCells.transform;
                goTmp.transform.localPosition = getCoordinates(GlobalData.startX,GlobalData.startZ);
                goTmp.transform.name = tmpMonsterNum.ToString();
                GlobalData.toSpawn = false;
                tmpMonsterNum++;
            }
        }
    }
    private void newLevel()
    {
        //monsterCountPerWave = Random.Range(level, level + GlobalData.X);
        GlobalData.monsterCountPerWave  = 1;
    }
    void Start()
    {
        diffX = (mapMatrixX/2);
        diffZ = (mapMatrixZ/2);
        tmpTime = tBW;
        newLevel();
        generateGameField();
    }

    void Update()
    {
        if(GlobalData.playerHP<=0)
        {
            towerDestroyed();
        }
        switch(GlobalData.timeMode)
        {
            case 'S': spawning();
            break;
            case 'W': waiting();
            break;
        }
    }
}
