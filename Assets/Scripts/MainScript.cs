using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public GameObject towerPanel;
    public Text txtTowerLevel; 
    public Text txtUpgradePrice; 
    public Text txtTowerDmg;
    public Text txtTowerAS; 
    public Text txtPlayerHP;
    public Text txtGold; 
    public Text txtNextWave;
    public GameObject gameOverPanel;
    public Text txtScore;
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
    private float tmpTime = 0;
    private bool gamePause = false;
    private Vector3 startPos;
    private Vector3 finishPos;
    private GameObject goTmp;

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
    public void newGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    private void towerDestroyed()
    {
        gameOverPanel.SetActive(true);
        txtScore.text = "Score: " + GlobalData.score;
    }
    private void waiting()
    {
        if(tmpTime<0)
        {
            tmpTime  = tBW;
            newLevel();
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
                GlobalData.monstersHP[tmpMonsterNum-1] = GlobalData.monsterHP;
                GlobalData.toSpawn = false;
                tmpMonsterNum++;
            }
        }
    }
    private void selectTower()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var hitCol = hitInfo.collider;
                if(hitCol != null)
                {
                    if(hitCol.tag == "Tower")
                    {
                        GlobalData.selectedTower = hitCol.gameObject;
                        var tmpSc = hitCol.gameObject.GetComponentInChildren<TowerScript>();
                        txtTowerDmg.text = tmpSc.getStatForUpgrade("D");
                        txtTowerAS.text = tmpSc.getStatForUpgrade("A");
                        txtUpgradePrice.text = tmpSc.getStatForUpgrade("G");
                        txtTowerLevel.text = tmpSc.getStatForUpgrade("L");
                        towerPanel.SetActive(true);
                    }
                    else
                    {
                        towerPanel.SetActive(false);
                    }
                }
            }
        }
    }
    private void newLevel()
    {
        GlobalData.monsterCountPerWave = Random.Range(level, level + GlobalData.X);
        ////Upgrade monsters
        int tmpSkillCount = Random.Range(1,4);
        GlobalData.monsterHP+=GlobalData.monsterAllStatsAdd;
        if(tmpSkillCount>1)
        GlobalData.monsterDeathGold+=GlobalData.monsterAllStatsAdd;
        if(tmpSkillCount>2)
        GlobalData.monsterDamage+=GlobalData.monsterAllStatsAdd;
        if(tmpSkillCount>3)
        GlobalData.monsterMovementSpeed+=GlobalData.monsterAllStatsAdd;
        GlobalData.monstersHP = new int[GlobalData.monsterCountPerWave];
        level ++;
    }
    public void towerUpgrade()
    {
        GlobalData.selectedTower.GetComponentInChildren<TowerScript>().towerUpgrade();
    }
    void Start()
    {
        GlobalData.reloadValues();
        diffX = (mapMatrixX/2);
        diffZ = (mapMatrixZ/2);
        tmpTime = tBW;
        generateGameField();
        txtNextWave.text = GlobalData.timeBetweenWaves.ToString();
    }

    void Update()
    {
        if(gamePause)
        return;

        txtPlayerHP.text = GlobalData.playerHP.ToString();
        txtGold.text = GlobalData.gold.ToString();
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
        selectTower();
    }
}
