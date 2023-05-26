using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitData : MonoBehaviour
{
    [Header("Unit Data")]
    public float health = 10;
    public float movementSpeed = 2;
    public bool hasCrystal = false;
    public int unitID = 0;
    public AudioClip enemyHitSound;
    public AudioClip enemyDeathSound;

    [Header("Visuals")]
    public GameObject enemyModel;
    private void Update()
    {
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);
            Destroy(gameObject);
        }
    }
    public void PlayerHitSound()
    {
        AudioSource.PlayClipAtPoint(enemyHitSound,transform.position);
    }
}
