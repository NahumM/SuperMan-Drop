using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform destanation;
    [SerializeField] GameObject camerapoint2;
    public List<GameObject> cameraPoints;

    private void Start()
    {
        destanation = transform;
        StartCoroutine("CameraStart");
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destanation.transform.position, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, destanation.transform.rotation, Time.deltaTime);
    }

    IEnumerator CameraStart()
    {
        destanation = cameraPoints[0].transform;
        yield return new WaitForSeconds(1.5f);
        destanation = cameraPoints[1].transform;
        yield return new WaitForSeconds(1.5f);
        destanation = cameraPoints[2].transform;
        yield return new WaitForSeconds(1.5f);
        destanation = cameraPoints[3].transform;
        yield return new WaitForSeconds(1.5f);
    }
}
