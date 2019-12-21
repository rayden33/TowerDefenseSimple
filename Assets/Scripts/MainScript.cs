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
    public int mapMatrixX = 10;
    public int mapMatrixZ = 10;

    private float diffX;
    private float diffZ;
    private Vector3 startPos;
    private Vector3 finishPos;
    private GameObject goTmp;
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
                    case 'S': goTmp = Instantiate(goStart); startPos = getCoordinates(i,j);
                    break;
                    case 'F': goTmp = Instantiate(goFinish); finishPos = getCoordinates(i,j);
                    break;
                }
                goTmp.transform.parent = goCells.transform;
                goTmp.transform.localPosition = getCoordinates(i,j);
            }
        }
    }
    void Start()
    {
        diffX = (mapMatrixX/2);
        diffZ = (mapMatrixZ/2);
        generateGameField();
    }

    void Update()
    {
        
    }
}
