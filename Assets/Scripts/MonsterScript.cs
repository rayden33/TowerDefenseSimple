using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public static int monsterHP;

    private char[,] mapTmp;
    private int mapMatrixX = GlobalData.mapMatrixX;
    private int mapMatrixZ = GlobalData.mapMatrixZ;
    private float diffX;
    private float diffZ;
    private Vector3 nextDir;
    private int curX,curZ,nextX,nextZ;
    

    private bool checkBorder(int x, int z)
    {
        return (x>=0 && x<mapMatrixX && z>=0 && z<mapMatrixZ);
    }
    private Vector3 getCoordinates(int x, int z)
    {
        return new Vector3(x - diffX, 0, z - diffZ);
    }
    private void createPersonalMap()
    {
        mapTmp = new char[mapMatrixX, mapMatrixZ];
        for(int i=0;i<mapMatrixX;i++)
        {
            for(int j=0;j<mapMatrixZ;j++)
            {
                mapTmp[i,j]=GlobalData.mapMatrix[i,j];
            }
        }
    }
    private void findNextPointTarget()
    {
        for(int i=-1;i<=1;i++)
        {
            for(int j=-1;j<=1;j++)
            {
                if(Mathf.Abs(i+j)==1 && checkBorder(curX+i,curZ+j))
                {
                    if(mapTmp[curX+i,curZ+j]=='W'||mapTmp[curX+i,curZ+j]=='F')
                    {
                        mapTmp[curX,curZ] = 'D';
                        nextX = curX+i;
                        nextZ = curZ+j;
                    }
                }
            }
        }
    }
    private void damageMainTower()
    {
        GlobalData.playerHP -=  GlobalData.monsterDamage;
        Debug.Log(GlobalData.playerHP);
        selfDestroy();
    }
    private void selfDestroy()
    {
        if(transform.gameObject.name == GlobalData.monsterCountPerWave.ToString())
        GlobalData.timeMode = 'W';
        Destroy(transform.gameObject);
    }
    void Start()
    {
        moveSpeed = GlobalData.monsterMovementSpeed;
        createPersonalMap();
        diffX = (mapMatrixX/2.0f);
        diffZ = (mapMatrixZ/2.0f);
        curX = GlobalData.startX;
        curZ = GlobalData.startZ;
        monsterHP = GlobalData.monstersHP[int.Parse(transform.name)-1];
        findNextPointTarget();

        //string str;
        // for(int i=0;i<mapMatrixX;i++)
        // {
        //     str = "";
        //     for(int j=0;j<mapMatrixZ;j++)
        //     {
        //         str = str + GlobalData.mapMatrix[i,j];
        //     }
        //     Debug.Log(str);
        // }
    }

    
    void Update()
    {
        if(GlobalData.monstersHP[int.Parse(transform.name)-1] <= 0)
        {
            selfDestroy();
        }
        if(Vector3.Distance(transform.position,getCoordinates(nextX,nextZ))<0.2f)
        {
            curX = nextX;
            curZ = nextZ;
            if(mapTmp[curX,curZ]=='F')
            {
                damageMainTower();
            }
            else
            {
                findNextPointTarget();
            }
        }
        else
        {
            nextDir = getCoordinates(nextX,nextZ) - transform.localPosition;
            transform.Translate(nextDir.normalized * Time.deltaTime * moveSpeed);
        }
    }
}
