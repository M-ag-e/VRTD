using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerUnitData : MonoBehaviour
{
    public enum TowerAttackOrder
    {
        First,
        Last,
        Strongest,
        Weakest
    }

    public enum TowerHitType 
    {
        Single,
        Multiple
    }

    [Header("Unit Data")]
    public TowerAttackOrder attackOrder = TowerAttackOrder.First;   // Order the tower will attack in
    public TowerHitType hitType = TowerHitType.Single;              // Type of projectile the tower will shoot (AoE or Single)
    public float towerAttackRate = 1;                               // Attack rate of tower
    public float towerDamage = 2;                                   // Tower damage
    public float towerRange = 5;                                    // Range of attack for the tower
    public Transform towerShootingPoint;                            // Point that the projectiles get launched from
    public GameObject towerProjectile;                              // Projectile to spawn
    public LayerMask enemyLayer;                                    // Enemy layer for sphere raycast collision


    private GameObject enemyTarget;                                 // Target for the tower to shoot
    private RaycastHit[] enemyRayHit;                               // Raycast variable for enemies hit with shpere cast check

    [Header("Debug Stuff")]
    public Color gizmoColour = new Color(1f,1f,1f,1f);              // Gizmo color set in inspector



    IEnumerator TowerLoop()
    {
       while (true)
        {
            enemyRayHit = Physics.SphereCastAll(towerShootingPoint.position,towerRange,Vector3.forward);
            switch (attackOrder)
            {
                case TowerAttackOrder.First:
                    enemyTarget = enemyRayHit[0].transform.gameObject;
                    foreach (RaycastHit hit in enemyRayHit) 
                    {
                        if (enemyTarget.GetComponent<EnemyUnitData>().unitID > hit.transform.gameObject.GetComponent<EnemyUnitData>().unitID)
                        {
                            Debug.Log($"{enemyTarget.GetComponent<EnemyUnitData>().unitID} is current target");
                            enemyTarget = hit.transform.gameObject;
                        }
                    }
                    break;

                case TowerAttackOrder.Last:
                    break;

                case TowerAttackOrder.Strongest:
                    break;
                
                case TowerAttackOrder.Weakest:
                    break;
                
                default:
                    Debug.Log("If you are reading this, i fucked up");
                    break;
            }

        }
        
    }

    public void TowerShoot(Transform transform)
    {
        // Spawns projectile, location set to shooting point, rotated towards enemy transform
        Instantiate(towerProjectile,towerShootingPoint.position,Quaternion.LookRotation(transform.forward));
    }

    private void OnDrawGizmosSelected()
    {
        // dont even ask, i hate unity color management :(
        Gizmos.color = new Color(gizmoColour.r,gizmoColour.g,gizmoColour.b,1f);

        // draws wire shphere to give a judgment on the towers radius
        Gizmos.DrawWireSphere(towerShootingPoint.position, towerRange);
    }
}
