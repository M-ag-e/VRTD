using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeTextManager : MonoBehaviour
{ 
    private void Start()
    {
        var text = GetComponent<TextMeshPro>();
        var tower = transform.parent.gameObject.GetComponent<TowerSpawnerData>();
        switch (tower.spawnerType)
        {
            case TowerSpawnerData.TowerSpawnerType.Basic:
                text.text = "";
                break;

            case TowerSpawnerData.TowerSpawnerType.Heavy:
                text.text = "";
                break;

            case TowerSpawnerData.TowerSpawnerType.AoE:
                text.text = "";
                break;
        }
    }
}
