using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossBehaviour : RagdollController
{
    [SerializeField] Slider hpSlider;
    [SerializeField] int hp;
    public void BossDamage(int amount)
    {
        hp -= amount;
        hpSlider.value = hp;
        if (hp <= 0)
        {
            final.MinusEnemy();
            oneTime = true;
            anim.SetBool("Die", true);
            GetComponent<NavMeshAgent>().enabled = false;
            this.enabled = false;
        }
    }
}
