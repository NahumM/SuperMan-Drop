using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    [SerializeField] ParticleSystem shootingParticle;
    [SerializeField] List<GameObject> patrolPoints = new List<GameObject>();
    int counter;
    public bool patrol;
    HeroController heroC;
    [SerializeField] GameObject restartButton;

    [SerializeField] bool goingToShoot;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 2) // Simple code to activate running animation
        {
            anim.SetBool("isMoving", true);
        }
        else anim.SetBool("isMoving", false);

        if (patrol)
        {
            Patroling();
            if (Vector3.Distance(transform.position, agent.destination) < 2f)
            {
                if (counter < patrolPoints.Count - 1)
                    counter++;
                else counter = 0;
            }
        }

        if (goingToShoot)
        {
            if (agent.isActiveAndEnabled)
            {
                if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
                {
                    anim.SetBool("isShooting", true);
                    shootingParticle.Play();
                    if (heroC != null)
                        heroC.Die();
                    goingToShoot = false;
                    restartButton.SetActive(true);
                }
                else anim.SetBool("isShooting", false);
            }
        }

    }

    public void FindHero(GameObject hero)
    {
        if (agent.isActiveAndEnabled)
        {
            agent.destination = transform.position + (hero.transform.position - transform.position).normalized;
            patrol = false;
        }
        heroC = hero.GetComponent<HeroController>();
        goingToShoot = true;
    }

    public void FindBody(GameObject hero)
    {
        if (agent.isActiveAndEnabled)
            agent.destination = transform.position + (hero.transform.position - transform.position).normalized;
        heroC = hero.GetComponent<HeroController>();
        goingToShoot = true;
    }


    void Patroling()
    {
        if (agent.enabled == true)
        agent.destination = patrolPoints[counter].transform.position;
    }
}
