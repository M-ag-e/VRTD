using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int livesRemaining = 50;
    public GameObject basicCube, heavyCube, AoECube;
    public Transform spawnArea;
    private void Update()
    {
        if (livesRemaining <= 0)
        {
            Debug.Log("Player ran out of lives!");
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            GameObject.Find("GamePathManager").GetComponent<EnemyPathManager>().StartWave();
        }

    }

    public void SpawnCubes()
    {
        for (int i = 0; i < Random.Range(0,2); i++)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    Instantiate(basicCube, spawnArea);
                    break;
                case 1:
                    Instantiate(heavyCube, spawnArea);
                    break; 
                case 2:
                    Instantiate(AoECube, spawnArea);
                    break;
            }
        }
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 150, 100), $"Ammount of lives remaining: {livesRemaining}");
    }
}
