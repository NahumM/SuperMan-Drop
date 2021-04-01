using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 destanation;
    Quaternion angleDestanation;
    [SerializeField] GameObject camerapoint2;

    private void Start()
    {
        destanation = transform.position;
        StartCoroutine("CameraStart");
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destanation, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, angleDestanation, Time.deltaTime);
    }

    IEnumerator CameraStart()
    {
        yield return new WaitForSeconds(2.0f);
        destanation = camerapoint2.transform.position;
        angleDestanation = camerapoint2.transform.rotation;
    }
}
