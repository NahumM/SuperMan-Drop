using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    GameObject arrows;
    bool isFindedByPlayer;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (CompareTag("Ball"))
        arrows = transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            if (rb.velocity.magnitude > 3f)
            {
                other.GetComponent<RagdollController>().TurnOnRagDoll();
                other.GetComponent<RagdollController>().RagDollExplotionForce(15000, transform.position, 3);
            }
        }
        if (other.CompareTag("FinalBoss"))
        {
            if (rb.velocity.magnitude > 3f)
            {
                other.GetComponent<BossBehaviour>().BossDamage(1);
            }
        }
    }

    public void DirectionArrow(Vector3 hitpoint)
    {
        Vector3 lookRotation = hitpoint;
        hitpoint.x = 0; hitpoint.z = 0;
        isFindedByPlayer = true;
        arrows.SetActive(true);
        arrows.transform.LookAt(2 * transform.position - lookRotation);
    }

    void LateUpdate()
    {
        if (CompareTag("Ball"))
        {
            if (!isFindedByPlayer)
                if (arrows != null)
                arrows.SetActive(false);
            isFindedByPlayer = false;
        }
    }

}
