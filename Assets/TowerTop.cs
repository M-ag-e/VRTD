using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTop : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {
        if (!gameObjects.Contains(other.gameObject))
        {
            gameObjects.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (gameObjects.Contains(other.gameObject))
        {
            gameObjects.Remove(other.gameObject);
        }
    }
}
