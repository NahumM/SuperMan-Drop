using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnHandler : MonoBehaviour
{
    [SerializeField] CameraController cameraC;
    [SerializeField] HeroController hero;
    [SerializeField] LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    public void StartButton(GameObject button)
    {
        button.SetActive(false);
        cameraC.StartLevel();

    }

    public void RestartButton()
    {
        levelLoader.RestartLevel();
    }

    public void NextButton()
    {
        levelLoader.LoadLevel();
    }

}
