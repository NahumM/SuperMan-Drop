using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialBeggining : MonoBehaviour
{
    HeroController heroC;

    [SerializeField] GameObject holdToAim;
    [SerializeField] GameObject releaseToAttack;
    bool stop;
    void Start()
    {
        heroC = GetComponent<HeroController>();
    }

    public void ShowHold(bool answer)
    {
        if (!stop)
        holdToAim.SetActive(answer);
    }

    public void ShowAttack(bool answer)
    {
        if (!stop)
        releaseToAttack.SetActive(answer);
    }

    public void StopTutorial()
    {
        holdToAim.SetActive(false);
        releaseToAttack.SetActive(false);
        stop = true;

    }

}
