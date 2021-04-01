using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] List<Rigidbody> ragdollParts = new List<Rigidbody>();
    Animator anim;

    void Start()
    {
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnRagDoll()
    {
        anim.enabled = false;

        foreach (Rigidbody rb in ragdollParts)
        {
            rb.isKinematic = false;
        }
    }

    public void RagDollExplotionForce(float exploationForce, Vector3 heroPosition, float blastRadius)
    {
        //rb.AddExplosionForce(explotionForce, transform.position, blastRadius);
    }
}
