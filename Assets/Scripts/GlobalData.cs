using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    /// G - ground, W - way, T - tower, S - spawn, F - finish
    public static char[,] mapMatrix = new char[,]{{'G','G','S','G','G','G','G','G','G','G'},
                                                  {'G','G','W','G','G','G','G','G','G','G'},
                                                  {'G','G','W','W','G','G','G','G','G','G'},
                                                  {'G','G','T','W','T','G','G','G','G','G'},
                                                  {'G','G','T','W','W','W','W','W','G','G'},
                                                  {'G','G','G','G','G','G','T','W','T','G'},
                                                  {'G','G','G','G','W','W','W','W','G','G'},
                                                  {'G','G','G','G','W','T','G','G','G','G'},
                                                  {'G','G','G','T','W','G','G','G','G','G'},
                                                  {'G','G','G','G','F','G','G','G','G','G'}}; //// Game map matrix
    public static int mapMatrixX = 10;      /// Map width
    public static int mapMatrixZ = 10;      /// Map height
    public static int startX;
    public static int startZ;
    public static int finishX;
    public static int finishZ;
    public static int playerHP;               //// Player main tower Health Points; 
    public static int gold;                    //// Player golds for upgrade towers;
    public static int score;                   //// Player count of destroyed monsters;
    public static int timeBetweenWaves;       //// Time between monster waves(sec); 

    public static int X;
    public static int[] monstersHP;
    public static int monsterHP;
    public static int monsterDamage;
    public static int monsterDeathGold;
    public static int monsterMovementSpeed;
    public static int monsterCountPerWave;
    public static int monsterAllStatsAdd;
    public static bool toSpawn = true;
    public static char timeMode;
    public static GameObject selectedTower;

    public static void reloadValues()
    {
        mapMatrixX = 10;
        mapMatrixZ = 10;
        playerHP = 10;
        gold = 0;
        score = 0;
        timeBetweenWaves = 2;
        X = 3;
        monsterHP = 10;
        monsterDamage = 1;
        monsterDeathGold = 1;
        monsterMovementSpeed = 1;
        monsterAllStatsAdd = 5;
        toSpawn = true;
        timeMode = 'W';
    }
}
