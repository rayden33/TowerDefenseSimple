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
    
    private int towerLevel = 1;
    private LineRenderer laserLineRenderer;
    private bool targetFind = false;
    private float secPerFire, tmpFireTime = 0, tmpTime = 0;
    private GameObject target;
    private Vector3 direction;

    private void towerUpgrade()
    {
        attackDamage *= upgradeAttackMultiplier;
        rateOfFire *= upgradeROFMultiplier;
    }
    private void fire()
    {
        direction = target.transform.position - transform.position;
        GlobalData.monstersHP[int.Parse(target.name)-1]--;
        Ray ray = new Ray( transform.position, direction );
        RaycastHit raycastHit;
        Vector3 endPosition = transform.position + ( direction );

        if( Physics.Raycast( ray, out raycastHit) ) {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition( 0, transform.position );
        laserLineRenderer.SetPosition( 1, endPosition);
        
    }
    void Start()
    {
        laserLineRenderer = GetComponent<LineRenderer>();
        secPerFire = 1.0f/rateOfFire;
    }

    void FixedUpdate()
    {
        if(targetFind)
        {
            if(tmpFireTime < 0)
            {
                tmpFireTime = secPerFire;
                try
                {
                    fire();
                }
                catch (System.Exception)
                {
                    targetFind = false;
                }
                
            }
            else
            {
                tmpFireTime-=Time.deltaTime;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.transform.tag == "Monster" && !targetFind)
        {
            targetFind = true;
            target = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject == target)
        {
            targetFind = false;
        }
    }
}
