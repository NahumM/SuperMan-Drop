using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroController : MonoBehaviour
{
    [SerializeField] float heightJump;
    [SerializeField] float jumpSpeed;
    float blastRadius = 3.5f;
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
    [SerializeField] GameObject trailSmoke;

    Material targetMat;

    [SerializeField] GameObject hostagesinDangerText;

    [HideInInspector] public bool isDead;

    Animator anim;

    bool inJump;

    bool touchPressed;

    bool canMove = true;

    public bool gameStarted;
    TutorialBeggining tutorialB;

    [SerializeField] bool tutorial;
    // Start is called before the first frame update
    void Start()
    {
        targetMat = targetObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        targetMat.color = new Color(1, 1, 1, 0.3f);
        if (tutorial)
        {
             tutorialB = GetComponent<TutorialBeggining>();
        }
        anim = GetComponent<Animator>();

        positionToMove = transform.position;
        jumpPoint = transform.position;
        positionInAir = transform.position;

        startXRotation = transform.eulerAngles.x;
    }

    public void StartHero()
    {
        gameStarted = true;
        if (tutorial)
        {
            tutorialB.ShowHold(true);
        }
    }


    void Update()
    {
        Time.timeScale += (1f / 2f) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        transform.position = Vector3.Lerp(transform.position, positionToMove, Time.deltaTime * jumpSpeed);

        if (!isDead && gameStarted && canMove)
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
                trailSmoke.SetActive(false);
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
        Collider[] colliders = Physics.OverlapSphere(jumpPoint, blastRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (collider.CompareTag("Enemy"))
            {
                if (collider.GetComponent<RagdollController>() != null)
                {
                    collider.GetComponent<RagdollController>().TurnOnRagDoll();
                    collider.GetComponent<RagdollController>().RagDollExplotionForce(explotionForce, transform.position, blastRadius);
                }
                if (tutorial)
                {
                    tutorialB.StopTutorial();
                }
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
                        if (child.gameObject.GetComponent<Rigidbody>() != null)
                        {
                            var rb2 = child.gameObject.GetComponent<Rigidbody>();
                            if (rb2 != null)
                                rb2.AddExplosionForce(explotionForce, transform.position, blastRadius);
                        }
                    }
                }
                collider.gameObject.GetComponent<BoxCollider>().enabled = false;

            }
            if (collider.CompareTag("ExplodeBox"))
            {
                if (collider.gameObject.GetComponent<ExploationBox>() != null)
                    collider.gameObject.GetComponent<ExploationBox>().Explode();
            }

            if (collider.CompareTag("Hostage"))
            {
                if (collider.gameObject.GetComponent<HostageController>() != null)
                    collider.gameObject.GetComponent<HostageController>().Die();
                collider.GetComponent<RagdollController>().TurnOnRagDoll();
                collider.GetComponent<RagdollController>().RagDollExplotionForce(explotionForce, transform.position, blastRadius);
            }

            if (collider.CompareTag("Dynomite"))
            {
                if (collider.gameObject.GetComponent<Dynamite>() != null)
                    collider.gameObject.GetComponent<Dynamite>().AttackBoss();
            }

            if (collider.CompareTag("FinalBoss"))
            {
                if (collider.gameObject.GetComponent<BossBehaviour>() != null)
                    collider.gameObject.GetComponent<BossBehaviour>().BossDamage(1);
            }
            if (collider.CompareTag("Ball"))
            {
                if (tutorial)
                {
                    tutorialB.StopTutorial();
                }
            }

            if (rb != null && collider.gameObject.layer != 7)
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
        restartButton.SetActive(true);
        canMove = false;
        positionToMove = new Vector3(transform.position.x, 0f, transform.position.z);
        hostagesinDangerText.GetComponent<TextMeshProUGUI>().text = "Mission failed! You died!";
        hostagesinDangerText.SetActive(true);
    }

    public void HostageDied()
    {
        isDead = true;
        restartButton.SetActive(true);
        canMove = false;
        positionToMove = new Vector3(transform.position.x, 0f, transform.position.z);
        hostagesinDangerText.GetComponent<TextMeshProUGUI>().text = "Mission failed! Hostage died!";
        hostagesinDangerText.SetActive(true);
    }

    public void Won()
    {
        positionToMove = new Vector3(transform.position.x, 0f, transform.position.z);
        canMove = false;
    }

    void Inputmouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tutorial)
            {
                tutorialB.ShowHold(false);
                tutorialB.ShowAttack(true);
            }
            trailSmoke.SetActive(true);
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
                if (tutorial)
                {
                    tutorialB.ShowAttack(false);
                    tutorialB.ShowHold(true);
                }
                anim.SetBool("isStanding", false);
                transform.eulerAngles = new Vector3(58f, transform.eulerAngles.y, transform.eulerAngles.z);
                inJump = true;
                jumpSpeed = 30;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, ~layerIgnore))
                {
                    if (!hit.collider.CompareTag("Cage"))
                    {
                        jumpPoint = hit.point;
                        positionToMove = new Vector3(jumpPoint.x, 0.1f, jumpPoint.z);
                    }
                    else inJump = false;
                }
                targetObject.SetActive(false);
                hostagesinDangerText.SetActive(false);
                touchPressed = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, ~layerIgnore))
            {
                targetObject.transform.position = hit.point;
                Collider[] colliders = Physics.OverlapSphere(hit.point, blastRadius);
                bool findHostage = false;
                bool findEnemy = false;

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Enemy") || collider.CompareTag("Boss"))
                    {
                        findEnemy = true;
                    }
                    if (collider.CompareTag("Hostage"))
                    {
                        hostagesinDangerText.SetActive(true);
                        findHostage = true;
                    }
                    if (collider.CompareTag("Ball"))
                    {
                        collider.GetComponent<BallBehaviour>().DirectionArrow(hit.point);
                    }
                }
                if (findEnemy) targetMat.color = new Color(0, 1, 0, 0.3f); else targetMat.color = new Color(1, 0, 0, 0.3f);
                if (findHostage)
                {
                    hostagesinDangerText.SetActive(true);
                    targetMat.color = new Color(1, 0, 0, 0.3f);
                }
                else hostagesinDangerText.SetActive(false);
                if (!findHostage && !findEnemy)
                {
                    targetMat.color = new Color(1, 1, 1, 0.3f);
                }
            }
        }

    }
}
