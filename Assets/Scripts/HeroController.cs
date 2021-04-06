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
    [SerializeField] GameObject particleCrack;
    [SerializeField] LayerMask layerIgnore;
    Vector3 positionInAir;
    Vector3 jumpPoint;
    Vector3 positionToMove;
    float startXRotation;
    [SerializeField] ParticleSystem shockwave;
    [SerializeField] GameObject restartButton;

    bool isDead;

    Animator anim;

    bool inJump;

    bool touchPressed;

    public bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        positionToMove = transform.position;
        jumpPoint = transform.position;
        positionInAir = transform.position;

        startXRotation = transform.eulerAngles.x;
    }


    void Update()
    {
        Time.timeScale += (1f / 2f) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        transform.position = Vector3.Lerp(transform.position, positionToMove, Time.deltaTime * jumpSpeed);

        if (!isDead && gameStarted)
        {
            Inputmouse();
            InJumpAction();
        }
    }

    void InJumpAction()
    {
        if (inJump)
        {
            if (Vector3.Distance(transform.position, positionToMove) < 2f)
            {
                anim.SetBool("isStanding", true);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                SlowMotion();
                Blast();
                Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
                Instantiate(particleCrack, pos, Quaternion.identity);
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
                        rb2.AddExplosionForce(explotionForce, transform.position, blastRadius);
                    }
                }
                collider.gameObject.GetComponent<BoxCollider>().enabled = false;

            }

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(explotionForce, transform.position, blastRadius);
            }
        }
    }

    void SlowMotion()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);
        positionToMove = new Vector3(transform.position.x, 0f, transform.position.z);
    }


    void Inputmouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPressed = true;
            jumpSpeed = 3;
            jumpPoint = transform.position;
            // positionInAir = jumpPoint + new Vector3(jumpPoint.x, jumpPoint.y + heightJump, jumpPoint.z);
            positionToMove = positionInAir;
            targetObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (touchPressed)
            {
                anim.SetBool("isStanding", false);
                transform.eulerAngles = new Vector3(58f, transform.eulerAngles.y, transform.eulerAngles.z);
                inJump = true;
                jumpSpeed = 30;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, ~layerIgnore))
                {
                    jumpPoint = hit.point;
                    positionToMove = new Vector3(jumpPoint.x, 0.1f, jumpPoint.z);
                }
                targetObject.SetActive(false);
                touchPressed = false;
            }
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
