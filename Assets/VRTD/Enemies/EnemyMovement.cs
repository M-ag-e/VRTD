using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyPathManager waypoints;
    [SerializeField] private float distanceThreshold;

    private float moveSpeed;
    private bool hasCrystal;
    private Transform currentWaypoint;

    private void Start()
    {
        waypoints = GameObject.Find("GamePathManager").GetComponent<EnemyPathManager>();
        moveSpeed = gameObject.GetComponent<EnemyUnitData>().movementSpeed;
        hasCrystal = gameObject.GetComponent<EnemyUnitData>().hasCrystal;

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint,hasCrystal);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint,hasCrystal);
        
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        Vector3 targetDirection = currentWaypoint.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, (moveSpeed*2)*Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint,hasCrystal);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TriggerManager>())
        {
            if (collision.gameObject.GetComponent<TriggerManager>().triggerType == TriggerManager.TriggerType.Crystal && !hasCrystal)
            {
                //Debug.Log("Enemy Picked up Crystal!!!!!!!!!");
                hasCrystal = true;
            }
            if (collision.gameObject.GetComponent<TriggerManager>().triggerType == TriggerManager.TriggerType.Entrance && hasCrystal)
            {
                //Debug.Log($"{GameManager.livesRemaining} lives remaining!");
                Destroy(gameObject);
            }
        }
    }
}
