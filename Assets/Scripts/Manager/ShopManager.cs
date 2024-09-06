using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TMP_Text textMoney;
    public int Money;
    public int select;
    public Text[] skinText;
    public int[] SkinPrice = new int[3];
    public int[] AllowSkin = new int[3];
    int IndexSelect;

    // Start is called before the first frame update
    void Start()
    {
        Money = PlayerPrefs.GetInt("Money");
        textMoney.text = Money.ToString();
        IndexSelect = PlayerPrefs.GetInt("skinSelect");
        for(int i =1;i< AllowSkin.Length; i++)
        {
            AllowSkin[i] = PlayerPrefs.GetInt("skin" + i);
        }
        UpdateSkin();
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClickSkin(int index)
    {
        if (index == IndexSelect) return;
        if(AllowSkin[index] == 1)
        {
            IndexSelect = index;
            PlayerPrefs.SetInt("skinSelect", index);
        }
        else if(Money>= SkinPrice[index])
        {
            AllowSkin[index] = 1;
            PlayerPrefs.SetInt("skin" + index, 1);
            Money -= SkinPrice[index];
            PlayerPrefs.SetInt("Money", Money);
            IndexSelect = index;
        }

        UpdateSkin();
    }

    void UpdateSkin()
    {
        for (int i = 0; i < skinText.Length; i++)
        {
            if (AllowSkin[i] == 1)
            {
                skinText[i].text = "Select";
            }
            else
            {
                skinText[i].text = SkinPrice[i].ToString();
            }
        }
        skinText[IndexSelect].text = "Selected";
    }
}
