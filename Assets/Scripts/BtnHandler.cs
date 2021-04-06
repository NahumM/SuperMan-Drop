using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnHandler : MonoBehaviour
{
    [SerializeField] CameraController cameraC;
    [SerializeField] HeroController hero;

   public void StartButton(GameObject button)
    {
        button.SetActive(false);
        cameraC.StartLevel();

    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

}
