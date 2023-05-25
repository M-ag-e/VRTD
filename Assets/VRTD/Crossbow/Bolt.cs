using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public TowerUnitData.TowerHitType hitType;
    public float projectileDamage = 0;
    public float projectileSpeed = 5;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.left * projectileSpeed, ForceMode.Impulse);
    }
    public void OnImpact()
    {
        Destroy(gameObject);
    }

    public void OnImpact(GameObject enemy)
    {
        // Add visual stuff later
        switch (hitType)
        {
            case TowerUnitData.TowerHitType.Single:
                enemy.GetComponent<EnemyUnitData>().health -= projectileDamage;
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
            OnImpact(collision.gameObject);
        }

    }
}
