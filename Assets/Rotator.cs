using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    float speed = 300f;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * speed);
    }
}
