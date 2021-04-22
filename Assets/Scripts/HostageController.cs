using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageController : MonoBehaviour
{

    Animator hostageAnimator;
    [SerializeField] HeroController heroC;


    private void Start()
    {
        hostageAnimator = GetComponent<Animator>();
    }
    public void Die()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        hostageAnimator.SetBool("isDead", true);
        heroC.HostageDied();
    }
}
