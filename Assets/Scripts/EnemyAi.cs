using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAi : MonoBehaviour
{ 
   public NavMeshAgent agent;
   public LayerMask whatIsGround, whatIsPlayer;

//Patroling
public Vector3 walkPoint;
bool walkPointSet;
public float walkPointRange;

//States
public float sightRange, attackRange;
public bool playerInSightRange, playerInAttackRange;

private void Awake()
{
    agent = GetComponent<NavMeshAgent>();
}
private void Update()
{
    if (!playerInSightRange && !playerInAttackRange) Patroling();
}
private void Patroling()
{
    if (!walkPointSet) SearchWalkPoint();

    if (walkPointSet)
        agent.SetDestination(walkPoint);

    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //Walkpoint reached
    if (distanceToWalkPoint.magnitude < 1f)
        walkPointSet = false;
}
private void SearchWalkPoint()
{
    //Calculate random point in range
    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    float randomX = Random.Range(-walkPointRange, walkPointRange);

    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
}
private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, sightRange);
}
}