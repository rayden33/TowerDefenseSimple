using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnScript : MonoBehaviour
{
    void OnTriggerExit(Collider col)
    {
        if(col.transform.tag == "Monster")
        {
            GlobalData.toSpawn = true;
        }
    }
}
