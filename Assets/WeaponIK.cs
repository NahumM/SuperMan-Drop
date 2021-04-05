using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIK : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject rightHandPos;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
