using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnerData : MonoBehaviour
{
    public enum TowerSpawnerType
    {
        Basic,
        Heavy,
        AoE
    }
    public TowerSpawnerType spawnerType;
}
