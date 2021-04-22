using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploationBox : MonoBehaviour
{

    public float blastRadius;
    public float explotionForce;
    bool delay;
    public ParticleSystem exploation;
    public void Explode()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        exploation.Play();
        exploation.gameObject.transform.parent = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (collider.CompareTag("Enemy"))
            {
                if (collider.GetComponent<RagdollController>() != null)
                    collider.GetComponent<RagdollController>().TurnOnRagDoll();
            }

            if (collider.CompareTag("Breakable"))
            {
                foreach (Transform child in collider.transform)
                {
                    if (child.gameObject.activeInHierarchy)
                        child.gameObject.SetActive(false);
                    else
                    {
                        child.gameObject.SetActive(true);
                        var rb2 = child.gameObject.GetComponent<Rigidbody>();
                        if (rb2 != null)
                            rb2.AddExplosionForce(explotionForce, transform.position, blastRadius);
                    }
                }
                collider.gameObject.GetComponent<BoxCollider>().enabled = false;

            }
            if (collider.CompareTag("ExplodeBox"))
            {
                StartCoroutine("BlastDelay", collider);
                delay = true;
            }

            if (collider.CompareTag("Hostage"))
            {
                if (collider.gameObject.GetComponent<HostageController>() != null)
                    collider.gameObject.GetComponent<HostageController>().Die();
                collider.GetComponent<RagdollController>().TurnOnRagDoll();
                collider.GetComponent<RagdollController>().RagDollExplotionForce(explotionForce, transform.position, blastRadius);
            }

            if (collider.CompareTag("Boss"))
            {
                if (collider.GetComponent<RagdollController>() != null)
                    collider.GetComponent<RagdollController>().TurnOnRagDoll();
            }

            if (collider.CompareTag("FinalBoss"))
            {
                if (collider.GetComponent<BossBehaviour>() != null)
                    collider.GetComponent<BossBehaviour>().BossDamage(3);
            }

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(explotionForce, transform.position, blastRadius);
            }
        }
        if (!delay)
            Destroy(this.gameObject);
    }

    IEnumerator BlastDelay(Collider collider)
    {
        yield return new WaitForSeconds(0.1f);
        if (collider != null)
        collider.gameObject.GetComponent<ExploationBox>().Explode();
        Destroy(this.gameObject);
    }
}

