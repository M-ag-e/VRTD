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

    [Header("Visuals")]
    public GameObject enemyModel;
}
