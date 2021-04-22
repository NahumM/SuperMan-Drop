using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTower : MonoBehaviour
{
    [SerializeField] GameObject targetHero;
    void Update()
    {
        Vector3 target = targetHero.transform.position;
        target.y = 0;
        transform.LookAt(target);
    }
}
