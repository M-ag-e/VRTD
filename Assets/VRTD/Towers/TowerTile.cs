using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    public BoxCollider boxTrigger;
    public GameObject towerSpawnPoint;

    public GameObject towerBasicPrefab;
    public GameObject towerHeavyPrefab;
    public GameObject towerAoEPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TowerSpawnerData>()) 
        {
            switch (other.gameObject.GetComponent<TowerSpawnerData>().spawnerType)
            {
                case TowerSpawnerData.TowerSpawnerType.Basic:
                    Instantiate(towerBasicPrefab, towerSpawnPoint.transform.position, Quaternion.identity, null);
                    break;

                case TowerSpawnerData.TowerSpawnerType.Heavy:
                    Instantiate(towerHeavyPrefab, towerSpawnPoint.transform.position, Quaternion.identity, null);
                    break;

                case TowerSpawnerData.TowerSpawnerType.AoE:
                    Instantiate(towerAoEPrefab, towerSpawnPoint.transform.position, Quaternion.identity, null);
                    break;

            }
            Destroy(other.gameObject);
        }
    }
}
