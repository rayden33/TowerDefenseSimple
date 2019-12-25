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

    public string getStatForUpgrade(string type)
    {
        string str="";
        if(type == "G")
        {
            str = "Price: " + upgradePrice.ToString();
        }
        else
        {
            if(type == "D")
            {
                str = attackDamage.ToString();
                str += " -> ";
                str += (attackDamage + upgradeAttackMultiplier).ToString();
            }
            else
            {
                if(type == "A")
                {
                    str = rateOfFire.ToString();
                    str += " -> ";
                    str += (rateOfFire + upgradeROFMultiplier).ToString();
                }
                else
                {
                    str = "Lvl: " + towerLevel.ToString();
                }
                
            }
        }
        return str;
    }
    public void towerUpgrade()
    {
        if(GlobalData.gold >= upgradePrice)
        {
            towerLevel ++;
            GlobalData.gold -= upgradePrice;
            attackDamage += upgradeAttackMultiplier;
            rateOfFire += upgradeROFMultiplier;
            secPerFire = 1.0f/rateOfFire;
        }
    }
    private void fire()
    {
        direction = target.transform.position - transform.position;
        GlobalData.monstersHP[int.Parse(target.name)-1]-=attackDamage;
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

    void Update()
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
            if(transform.name == "tower_0")
            Debug.Log(secPerFire);
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
