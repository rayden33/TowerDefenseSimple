using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public int attackDamage = 1;                        /// tower attack damage
    public float rateOfFire = 1.0f;                          /// tower count of attack per second
    public int upgradePrice = 10;                       /// price for upgrade tower
    public int upgradeAttackMultiplier = 2;             /// multiplier attack damage after upgrade (1 - don't raise)
    public float upgradeROFMultiplier = 1.0f;                /// multiplier rate of fire after upgrade (1 - don't raise)
    
    private bool targetFind = false;
    private float secPerFire, tmpFireTime = 0, tmpTime = 0;
    private GameObject target;

    private void fire()
    {
        
    }
    void Start()
    {
        secPerFire = 1.0f/rateOfFire;
    }

    void Update()
    {
        if(targetFind)
        {
            if(tmpFireTime < 0)
            {
                tmpFireTime = secPerFire;
                fire();
            }
            else
            {
                tmpFireTime-=Time.deltaTime;
            }
        }
    }
}
