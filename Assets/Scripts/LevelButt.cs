using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButt : MonoBehaviour
{
    // Start is called before the first frame update
    public string NameLevel = "Level";
    public Image[] LightBolt;
    public Sprite EnebleLightBolt;
    public Text MoneyText;
    public int Money;
    int isOpen;
    LevelsMenu levelsMenu;
    void Start()
    {
        levelsMenu = GameObject.FindAnyObjectByType<LevelsMenu>();
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt(NameLevel + "lightBolt" + i) == 1)
            {
                LightBolt[i].sprite = EnebleLightBolt;
            }
        }
        isOpen = PlayerPrefs.GetInt(NameLevel + "open");
        if (isOpen == 1)
        {
            MoneyText.text = "Play";
        }
        else
        {
            MoneyText.text = Money.ToString();
        }
        if (Money == -1)
        {
            MoneyText.text = "Play";
            isOpen = 1;
        }
    }

    public void Click()
    {
        if (isOpen == 1)
        {
            SceneManager.LoadScene(NameLevel);
        }
        else
        {
            if (levelsMenu.Buy(Money))
            {
                PlayerPrefs.SetInt(NameLevel + "open",1);
                isOpen = 1;
                MoneyText.text = "Play";
            }
        }
    }
}
