using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    Animator anim;
    [SerializeField] ParticleSystem blastP;
    [SerializeField] BossBehaviour boss;
    [SerializeField] GameObject trail;
    [SerializeField] GameObject tutorialParticle;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }
    public void AttackBoss()
    {
        anim.SetBool("Attack", true);
        trail.SetActive(true);
        tutorialParticle.SetActive(false);
        StartCoroutine("BlastDelay");
    }

    IEnumerator BlastDelay()
    {
        yield return new WaitForSeconds(0.5f);
        blastP.Play();
        blastP.gameObject.transform.parent = null;
        boss.BossDamage(1);
        Destroy(this.gameObject);
    }
}
