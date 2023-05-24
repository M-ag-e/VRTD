using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    public TowerUnitData.TowerHitType hitType;
    public float projectileSpeed = 5;


    public void OnImpact()
    {
        // Add visual stuff later
        switch (hitType)
        {
            case TowerUnitData.TowerHitType.Single:
                // Do single damage
                break;
            case TowerUnitData.TowerHitType.Multiple:
                // Do area damage
                break;
            default:
                // Nothing should ever call this, but adding it for bug catching
                Debug.LogError("Somthing has gone terribly wrong!");
                break;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            // Do nothing, go stright to impact
            OnImpact();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Damage enemy, then impact
            OnImpact();
        }

    }
}
