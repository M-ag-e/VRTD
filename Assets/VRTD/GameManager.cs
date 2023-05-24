using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int livesRemaining = 50;
    private void Update()
    {
        if (livesRemaining <= 0)
        {
            Debug.Log("Player ran out of lives!");
        }
    }
}
