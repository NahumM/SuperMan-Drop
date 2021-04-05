using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    [SerializeField] List<GameObject> patrolPoints = new List<GameObject>();
    int counter;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Patroling();
        if (agent.velocity.magnitude > 2) // Simple code to activate running animation
        {
            anim.SetBool("isMoving", true);
        }
        else anim.SetBool("isMoving", false);

        if (Vector3.Distance(transform.position, agent.destination) < 2f)
        {
            if (counter < patrolPoints.Count - 1)
                counter++;
            else counter = 0;
        }

       

    }


    void Patroling()
    {
        if (agent.enabled == true)
        agent.destination = patrolPoints[counter].transform.position;
    }
}
