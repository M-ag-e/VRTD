using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    public TowerUnitData.TowerHitType hitType;
    public float projectileDamage = 0;
    public float projectileSpeed = 5;
    //public Color projectileColor;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        //ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        //ParticleSystem.MainModule ma = ps.main;
        StartCoroutine(KillAfterSeconds());
        //ma.startColor = projectileColor;
        //gameObject.GetComponentInChildren<Light>().color = projectileColor;
    }
    private void FixedUpdate()
    {
        Vector3 velocity = Vector3.forward * projectileSpeed;
        transform.Translate(velocity * Time.deltaTime);
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
    IEnumerator KillAfterSeconds()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
