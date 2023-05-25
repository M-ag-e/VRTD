using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    public float towerProjectileSpeed = 5f;                         // Speed of projectile the tower shoots
    
    public Transform towerShootingPoint;                            // Point that the projectiles get launched from
    public GameObject towerProjectile;                              // Projectile to spawn
    public LayerMask enemyLayer;                                    // Enemy layer for sphere raycast collision


    [SerializeField] private GameObject enemyTarget = null;         // Target for the tower to shoot
    private List<GameObject> enemyObjects;
    private TowerTop towerTop;
    [Header("Debug Stuff")]
    public Color gizmoColour = new Color(1f,1f,1f,1f);              // Gizmo color set in inspector

    private void Start()
    {
        GetComponentInChildren<SphereCollider>().radius = towerRange / 2;
        towerTop = GetComponentInChildren<TowerTop>();
        StartCoroutine(TowerLoop());
    }

    IEnumerator TowerLoop()
    {
       while (true)
        {
            bool canfire = true;
            enemyTarget = null;
            enemyObjects = towerTop.gameObjects;
            switch (attackOrder)
            {
                case TowerAttackOrder.First:
                    
                    if (enemyObjects != null)
                    {
                        foreach (GameObject obj in enemyObjects)
                        {
                            if (obj != null && obj.GetComponent<EnemyUnitData>())
                            {
                                if (!obj.GetComponent<EnemyUnitData>().hasCrystal)
                                {
                                    if (enemyTarget == null || obj.GetComponent<EnemyUnitData>().unitID <= enemyTarget.GetComponent<EnemyUnitData>().unitID)
                                    {
                                        if (canfire)
                                        {
                                            canfire = false;
                                            enemyTarget = obj;
                                            Debug.Log($"Targeting enemy {enemyTarget.GetComponent<EnemyUnitData>().unitID} at ({Time.deltaTime})");
                                            TowerShoot(enemyTarget.transform);
                                        }
                                    }
                                }                                               // World trade centre memorial in c#
                                else
                                {
                                    if (enemyTarget == null || obj.GetComponent<EnemyUnitData>().unitID >= enemyTarget.GetComponent<EnemyUnitData>().unitID)
                                    {
                                        if (canfire)
                                        {
                                            canfire = false;
                                            enemyTarget = obj;
                                            Debug.Log($"Targeting enemy {enemyTarget.GetComponent<EnemyUnitData>().unitID} at ({Time.deltaTime})");
                                            TowerShoot(enemyTarget.transform);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    break;

                case TowerAttackOrder.Last:
                    if (enemyObjects != null)
                    {
                        foreach (GameObject obj in enemyObjects)
                        {
                            if (obj != null && obj.GetComponent<EnemyUnitData>())
                            {
                                if (!obj.GetComponent<EnemyUnitData>().hasCrystal)
                                {
                                    if (enemyTarget == null || obj.GetComponent<EnemyUnitData>().unitID >= enemyTarget.GetComponent<EnemyUnitData>().unitID)
                                    {
                                        if (canfire)
                                        {
                                            canfire = false;
                                            enemyTarget = obj;
                                            Debug.Log($"Targeting enemy {enemyTarget.GetComponent<EnemyUnitData>().unitID} at ({Time.deltaTime})");
                                            TowerShoot(enemyTarget.transform);
                                        }
                                    }
                                }                                               // World trade centre memorial in c#
                                else
                                {
                                    if (enemyTarget == null || obj.GetComponent<EnemyUnitData>().unitID <= enemyTarget.GetComponent<EnemyUnitData>().unitID)
                                    {
                                        if (canfire)
                                        {
                                            canfire = false;
                                            enemyTarget = obj;
                                            Debug.Log($"Targeting enemy {enemyTarget.GetComponent<EnemyUnitData>().unitID} at ({Time.deltaTime})");
                                            TowerShoot(enemyTarget.transform);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                case TowerAttackOrder.Strongest:
                    break;

                case TowerAttackOrder.Weakest:
                    break;

                default:
                    Debug.Log("If you are reading this, i fucked up");
                    break;
            
            }
            enemyObjects = null;
            yield return new WaitForSeconds(towerAttackRate);
        }
        
    }

    public void TowerShoot(Transform transform)
    {
        // Spawns projectile, location set to shooting point, rotated towards enemy transform
        towerShootingPoint.LookAt(transform.position);
        var obj = Instantiate(towerProjectile,towerShootingPoint.position,Quaternion.LookRotation(towerShootingPoint.forward));
        obj.GetComponent<TowerProjectile>().projectileDamage = towerDamage;
        obj.GetComponent<TowerProjectile>().projectileSpeed = towerProjectileSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        // dont even ask, i hate unity color management :(
        Gizmos.color = new Color(gizmoColour.r,gizmoColour.g,gizmoColour.b,1f);

        // draws wire shphere to give a judgment on the towers radius
        Gizmos.DrawWireSphere(towerShootingPoint.position, towerRange);
    }
}
