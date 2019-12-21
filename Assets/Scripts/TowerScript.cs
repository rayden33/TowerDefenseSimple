using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public int attackDamage = 1;                        /// tower attack damage
    public int rateOfFire = 1;                          /// tower count of attack per second
    public int upgradePrice = 10;                       /// price for upgrade tower
    public int upgradeAttackMultiplier = 2;             /// multiplier attack damage after upgrade (1 - don't raise)
    public int upgradeROFMultiplier = 1;                /// multiplier rate of fire after upgrade (1 - don't raise)
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
