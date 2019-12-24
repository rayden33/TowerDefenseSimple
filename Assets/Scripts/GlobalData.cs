using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    /// G - ground, W - way, T - tower, S - spawn, F - finish
    public static char[,] mapMatrix = new char[,]{{'G','G','S','G','G','G','G','G','G','G'},
                                                  {'G','G','W','T','G','G','G','G','G','G'},
                                                  {'G','G','W','W','G','G','G','G','G','G'},
                                                  {'G','G','T','W','T','G','G','G','G','G'},
                                                  {'G','G','T','W','W','W','W','W','G','G'},
                                                  {'G','G','G','G','G','G','T','W','T','G'},
                                                  {'G','G','G','G','W','W','W','W','G','G'},
                                                  {'G','G','G','G','W','T','G','G','G','G'},
                                                  {'G','G','G','T','W','G','G','G','G','G'},
                                                  {'G','G','G','G','F','G','G','G','G','G'}}; //// Game map matrix
    public static int[] monstersHP;
    public static int mapMatrixX = 10;
    public static int mapMatrixZ = 10;
    public static int startX;
    public static int startZ;
    public static int finishX;
    public static int finishZ;
    public static float[,] mapMatrixCordinates = new float[10,10];
    public static int playerHP = 40;               //// Player main tower Health Points; 
    public static int gold = 0;                    //// Player golds for upgrade towers;
    public static int score = 0;                   //// Player count of destroyed monsters;
    public static int timeBetweenWaves = 3;       //// Time between monster waves(sec); 

    public static int X = 5;
    public static int monsterHP = 10;
    public static int monsterDamage = 1;
    public static int monsterDeathGold = 1;
    public static int monsterCountPerWave;
    public static bool toSpawn = true;
    public static char timeMode = 'W';
    public static Queue monsterRoute = new Queue();
}
