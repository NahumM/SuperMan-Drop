using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> levelPrefabs = new List<GameObject>();
    GameObject loadedLevel;
    int levelCounter;
    bool gameFinal;

    private void Awake()
    {
        levelCounter = PlayerPrefs.GetInt("Level");
        LoadLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            LoadLevel();
        }
    }


    public void LoadLevel()
    {
        if (loadedLevel != null)
            Destroy(loadedLevel);
        if (levelCounter < levelPrefabs.Count)
        {
            loadedLevel = Instantiate(levelPrefabs[levelCounter]);
            levelCounter++;
        }
        else
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void RestartLevel()
    {
        Destroy(loadedLevel);
        loadedLevel = Instantiate(levelPrefabs[levelCounter - 1]);
    }


    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", levelCounter - 1);
        PlayerPrefs.Save();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            PlayerPrefs.SetInt("Level", levelCounter - 1);
            PlayerPrefs.Save();
        }
    }

}
