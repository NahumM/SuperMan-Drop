using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> levelPrefabs = new List<GameObject>();
    GameObject loadedLevel;
    int levelCounter;
    bool gameFinal;
    bool passed3thLevel;
    bool passed4thLevel;
    bool rewardedAdShown;

    private void Start()
    {
        levelCounter = PlayerPrefs.GetInt("Level");
        passed4thLevel = LoadBool("Passed4thLevel");
        passed3thLevel = LoadBool("Passed3thLevel");
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
            if (levelCounter >= 2)
                passed3thLevel = true;
            if (levelCounter >= 3)
                passed4thLevel = true;
            if (!rewardedAdShown)
            {
                if (passed4thLevel)
                    SDKManager.sdkManager.ShowBanner();
            }
            else rewardedAdShown = false;
            if (passed3thLevel)
                SDKManager.sdkManager.ShowAd();
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
        if (passed3thLevel)
            SDKManager.sdkManager.ShowAd();
        Destroy(loadedLevel);
        loadedLevel = Instantiate(levelPrefabs[levelCounter - 1]);
        GameObject.Find("Button_Blue").GetComponent<Button>().onClick.Invoke();
    }

    void SaveBool(string name, bool answer)
    {
        if (answer)
            PlayerPrefs.SetInt(name, 1);
        else PlayerPrefs.SetInt(name, 0);
    }

    bool LoadBool(string name)
    {
        if (PlayerPrefs.GetInt(name) == 0)
            return false;
        else return true;
    }

    public void SkipLevelForAd()
    {
        SDKManager.sdkManager.SkipLevelRV();
    }

    public void OnRewardedEvent()
    {
        rewardedAdShown = true;
        LoadLevel();
    }





    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", levelCounter - 1);
        SaveBool("Passed4thLevel", passed4thLevel);
        SaveBool("Passed3thLevel", passed3thLevel);
        PlayerPrefs.Save();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            PlayerPrefs.SetInt("Level", levelCounter - 1);
            SaveBool("Passed4thLevel", passed4thLevel);
            SaveBool("Passed3thLevel", passed3thLevel);
            PlayerPrefs.Save();
        }
    }

}
