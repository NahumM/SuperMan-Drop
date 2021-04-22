using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollController : MonoBehaviour
{
    [SerializeField] List<Rigidbody> ragdollParts = new List<Rigidbody>();
    protected Animator anim;
    FieldOfView view;
    NavMeshAgent agent;
    [SerializeField] protected FinalController final;
    Rigidbody rb;
    protected bool oneTime;
    [SerializeField] GameObject tower;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<FieldOfView>();
       anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TurnOnRagDoll()
    {
        if (!oneTime)
        {
            if (CompareTag("Enemy") || CompareTag("Boss"))
            {
                final.MinusEnemy();
                view.meshResolution = 0;
            }
            anim.enabled = false;
            if (agent != null)
                agent.enabled = false;

            foreach (Rigidbody rb in ragdollParts)
            {
                rb.isKinematic = false;
            }
            this.enabled = false;
            oneTime = true;
            if (tower != null)
            {
                tower.GetComponent<TankTower>().enabled = false;
            }
        }
    }

    public void RagDollExplotionForce(float exploationForce, Vector3 heroPosition, float blastRadius)
    {
        foreach (Rigidbody rb in ragdollParts)
        {
            rb.AddExplosionForce(exploationForce, transform.position, blastRadius);
        }
    }
}
