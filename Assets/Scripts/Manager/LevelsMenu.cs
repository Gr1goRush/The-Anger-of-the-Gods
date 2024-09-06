using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelsMenu : MonoBehaviour
{
    public TMP_Text textMoney;
    public int Money;
    //public static LevelsMenu singenton;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Money = PlayerPrefs.GetInt("Money");
        textMoney.text = Money.ToString();
    }

    public bool Buy(int price)
    {
        if(price <= Money)
        {
            Money -= price;
            textMoney.text = Money.ToString();
            PlayerPrefs.SetInt("Money", Money);
            return true;
        }
        return false;
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
