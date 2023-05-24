using System.Collections;
using System.Collections.Generic;
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
    
    private GameObject[] enemiesInRange;                            // List of all eneimes, going to update on when enemies enter/leave range

    [Header("Debug Stuff")]
    public Color gizmoColour = new Color(1f,1f,1f,1f);              // Gizmo color set in inspector
    

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
