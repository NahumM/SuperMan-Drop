using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalController : MonoBehaviour
{
    [SerializeField] float enemiesToKill;
    [SerializeField] CameraController cameraC;
    [SerializeField] List<Animator> hostales;
    [SerializeField] HeroController hero;
    [SerializeField] GameObject restartButton;

    public void MinusEnemy()
    {
        enemiesToKill--;
        if (enemiesToKill == 0)
        {
            StartCoroutine("Final");
        }
    }

    IEnumerator Final()
    {
        cameraC.destanation = cameraC.cameraPoints[0].transform;
        hero.Die();
        yield return new WaitForSeconds(2.0f);
        foreach (Animator anim in hostales)
        {
            anim.gameObject.transform.position = new Vector3(anim.gameObject.transform.position.x, anim.gameObject.transform.position.y + 0.4f, anim.gameObject.transform.position.z);
            anim.SetBool("isFinal", true);
        }
        yield return new WaitForSeconds(1.0f);
        restartButton.SetActive(true);
    }
}
