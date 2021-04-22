using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform destanation;
    [SerializeField] GameObject camerapoint2;
    public Transform cameraWin;
    public List<GameObject> cameraPoints;
    public HeroController hero;
    [SerializeField] List<ParticleSystem> cryParticles = new List<ParticleSystem>();
    [SerializeField] List<ParticleSystem> angryParticles = new List<ParticleSystem>();

    private void Start() => destanation = transform;
    public void StartLevel() => StartCoroutine("CameraHandler");

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destanation.transform.position, Time.deltaTime * 3);
        transform.rotation = Quaternion.Lerp(transform.rotation, destanation.transform.rotation, Time.deltaTime * 3);
    }

    IEnumerator CameraStart()
    {
        destanation = cameraPoints[0].transform;
        yield return new WaitForSeconds(1.5f);
        CryParticles();
        yield return new WaitForSeconds(0.2f);
        AngryParticles(0); AngryParticles(1);
        yield return new WaitForSeconds(1.0f);
        destanation = cameraPoints[1].transform;
        yield return new WaitForSeconds(1.5f);
        AngryParticles(2);
        yield return new WaitForSeconds(0.3f);
        destanation = cameraPoints[2].transform;
        yield return new WaitForSeconds(1.5f);
        AngryParticles(3);
        yield return new WaitForSeconds(0.3f);
        destanation = cameraPoints[3].transform;
        yield return new WaitForSeconds(3f);
        hero.StartHero();
    }

    IEnumerator CameraHandler()
    {
        foreach (GameObject point in cameraPoints)
        {
            destanation = point.transform;
            yield return new WaitForSeconds(0.2f);
        }
        hero.StartHero();
    }

    void CryParticles()
    {
        foreach (ParticleSystem particle in cryParticles)
        {
            particle.Play();
        }
    }

    void AngryParticles(int i)
    {
        angryParticles[i].Play();
    }

}
