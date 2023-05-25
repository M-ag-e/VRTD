using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    public GameObject basicEnemyPrefab;         // int 0
    public GameObject fastEnemyPrefab;          // int 1
    public GameObject tankEnemyPrefab;          // int 2


    private int[] wave0 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] wave1 = new int[] { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0 };
    private int[] wave2 = new int[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
    private int[] wave3 = new int[] { 0, 2, 1, 0, 0, 1, 0, 2, 1, 0, 0 };
    private int[] wave4 = new int[] { 0, 2, 1, 0, 1, 1, 2, 0, 1, 2, 1 };

    private List<GameObject> gameObjects = new List<GameObject>();
    private int waveCount = 0;
    public static bool canWaveBeStarted = true;

    private void Start()
    {

    }

    private void Update()
    {
        if (gameObjects.Count == 0)
        {
            StartCoroutine(WaitEndOfRound());
        }
        if (canWaveBeStarted)
        {
            StartWave();
        }
    }
    private void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {
            
            if (t == transform.GetChild(0) || t == transform.GetChild(transform.childCount-1))
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.blue;
            }
            Gizmos.DrawWireCube(t.position, new Vector3(1f, 1f, 1f));

        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        
    }

    public Transform GetNextWaypoint(Transform currentWaypoint, bool hasCystal)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }
        if (!hasCystal)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }

        if (hasCystal)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex()-1);
        }
        else 
        {
            Debug.LogError("uh oh, somthing broke here");
            return null;
        }
    }
    public void StartWave()
    {
        if (canWaveBeStarted && waveCount < 5)
        {
            canWaveBeStarted = false;
            switch (waveCount)
            {
                case 0:
                    StartCoroutine(WaitToSpawnEnemies(wave0));
                    waveCount++;
                    break;

                case 1:
                    StartCoroutine(WaitToSpawnEnemies(wave1));
                    waveCount++;
                    break;

                case 2:
                    StartCoroutine(WaitToSpawnEnemies(wave2));
                    waveCount++;
                    break;

                case 3:
                    StartCoroutine(WaitToSpawnEnemies(wave3));
                    waveCount++;
                    break;

                case 4:
                    StartCoroutine(WaitToSpawnEnemies(wave4));
                    waveCount++;
                    break;


                default: Debug.LogError("This shouldnt be called"); break;
            }
        }
    }
    IEnumerator WaitToSpawnEnemies(int[] wave)
    {
        
        for (int i = 0;i < wave.Length - 1; i++) 
        {
            GameObject obj = null;
            switch (wave[i])
            {
                case 0:
                    obj = Instantiate(basicEnemyPrefab);
                    break;
                case 1:
                    obj = Instantiate(fastEnemyPrefab);
                    break;
                case 2:
                    obj = Instantiate(tankEnemyPrefab);
                    break;

                default:
                    Debug.Log("This should not call");
                    break;
            }
            gameObjects.Add(obj);
            obj.GetComponent<EnemyUnitData>().unitID = i;
            obj.name += $" ({i})";
            yield return new WaitForSeconds(1f); 
        }
    }
    IEnumerator WaitEndOfRound()
    {
        yield return new WaitForSeconds(10);
        canWaveBeStarted = true;
    }
}
