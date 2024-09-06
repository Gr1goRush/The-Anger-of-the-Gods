using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.Load();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void JumpButton()
    {
        string nameLevel = PlayerPrefs.GetString("LastLevel");
        nameLevel = (nameLevel == "") ? "Level1" : nameLevel;
        SceneManager.LoadScene(nameLevel);
    }
}
