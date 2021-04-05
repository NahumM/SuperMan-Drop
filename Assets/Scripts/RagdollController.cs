using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollController : MonoBehaviour
{
    [SerializeField] List<Rigidbody> ragdollParts = new List<Rigidbody>();
    Animator anim;
    FieldOfView view;
    NavMeshAgent agent;
    [SerializeField] FinalController final;

    void Start()
    {
        view = GetComponent<FieldOfView>();
       anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TurnOnRagDoll()
    {
        final.MinusEnemy();
        anim.enabled = false;
        view.meshResolution = 0;
        if (agent != null)
            agent.enabled = false;

        foreach (Rigidbody rb in ragdollParts)
        {
            rb.isKinematic = false;
        }
        this.enabled = false;
    }

    public void RagDollExplotionForce(float exploationForce, Vector3 heroPosition, float blastRadius)
    {
        //rb.AddExplosionForce(explotionForce, transform.position, blastRadius);
    }
}
