using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
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
        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1 && !hasCystal)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        /**
        if (currentWaypoint.GetSiblingIndex() >= transform.childCount -1 && hasCystal)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() - 1);
        }
        **/
        else if (!hasCystal)
        {
            return transform.GetChild(0);
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
}
