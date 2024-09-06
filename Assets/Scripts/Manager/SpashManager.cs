using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpashManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        if (PlayerPrefs.GetInt("one") == 0)
        {
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.SetInt("effect", 1);
            PlayerPrefs.SetInt("vibro", 1);
            PlayerPrefs.SetInt("volume", 10);
            PlayerPrefs.SetInt("one", 1);
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
