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
                text.text = "Basic Tower\nMedium Firerate\nMedium Damage";
                break;

            case TowerSpawnerData.TowerSpawnerType.Heavy:
                text.text = "Heavy Tower\nSlow Firerate\nHigh Damage";
                break;

            case TowerSpawnerData.TowerSpawnerType.AoE:
                text.text = "Light Tower\nFast Firerate\nLow Damage";
                break;
        }
    }
}
