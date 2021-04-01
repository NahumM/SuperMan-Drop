using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] float heightJump;
    [SerializeField] float jumpSpeed;
    [SerializeField] float blastRadius = 5f;
    [SerializeField] float explotionForce = 700f;
    [SerializeField] GameObject targetObject;
    Vector3 positionInAir;
    Vector3 jumpPoint;
    Vector3 positionToMove;
    [SerializeField] ParticleSystem shockwave;

    bool inJump;
    // Start is called before the first frame update
    void Start()
    {
        positionToMove = transform.position;
        jumpPoint = transform.position;
        positionInAir = transform.position;
    }


    void Update()
    {
        Inputmouse();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSpeed = 3;
            jumpPoint = transform.position;
           // positionInAir = jumpPoint + new Vector3(jumpPoint.x, jumpPoint.y + heightJump, jumpPoint.z);
            positionToMove = positionInAir;
        }

        transform.position = Vector3.Lerp(transform.position, positionToMove, Time.deltaTime * jumpSpeed);

            if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpSpeed = 30;
            positionToMove = jumpPoint;
        }

        if (inJump)
        {
            if (Vector3.Distance(transform.position, positionToMove) < 1f)
            {
                Blast();
                shockwave.Play();
                inJump = false;
            }
        }
    }

    void Blast()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<RagdollController>().TurnOnRagDoll();
            }

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(explotionForce, transform.position, blastRadius);
            }
        }
    }


    void Inputmouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jumpSpeed = 3;
            jumpPoint = transform.position;
            // positionInAir = jumpPoint + new Vector3(jumpPoint.x, jumpPoint.y + heightJump, jumpPoint.z);
            positionToMove = positionInAir;
            targetObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            inJump = true;
            jumpSpeed = 30;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                jumpPoint = hit.point;
                positionToMove = jumpPoint;
            }
            targetObject.SetActive(false);
        }
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                targetObject.transform.position = hit.point;
            }
        }

    }
}
