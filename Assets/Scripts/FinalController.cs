using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalController : MonoBehaviour
{
    float enemiesToKill;
    [SerializeField] CameraController cameraC;
    [SerializeField] List<Animator> hostales;
    [SerializeField] HeroController hero;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject nextButton;


    private void Start()
    {
        StartCoroutine("LateStart");
    }

    public void MinusEnemy()
    {
        enemiesToKill--;
        if (enemiesToKill <= 0)
        {
            StartCoroutine("Final");
        }
    }

    IEnumerator Final()
    {
        var finalMessage = GameObject.Find("SaveHostages_text");
        if (finalMessage != null)
        {
            var textMesh = finalMessage.GetComponent<TextMeshProUGUI>();
            textMesh.text = "Mission Complete!";
            textMesh.color = Color.green;
        }
        yield return new WaitForSeconds(1f);
        nextButton.SetActive(true);
        cameraC.destanation = cameraC.cameraWin;
        hero.Won();
        yield return new WaitForSeconds(2.0f);
        if (!hero.isDead)
        {
            foreach (Animator anim in hostales)
            {
                Vector3 lookPos = cameraC.cameraWin.position;
                lookPos.y = 0;
                anim.gameObject.transform.LookAt(lookPos);
                anim.gameObject.transform.position = new Vector3(anim.gameObject.transform.position.x, anim.gameObject.transform.position.y + 0.4f, anim.gameObject.transform.position.z);
                anim.SetBool("isFinal", true);
            }
        }
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        GameObject[] final = GameObject.FindGameObjectsWithTag("FinalBoss");
        enemiesToKill = enemies.Length + bosses.Length + final.Length;
    }
}
