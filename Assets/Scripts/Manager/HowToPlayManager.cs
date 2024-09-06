using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    int index;
    public Image image;
    public Sprite[] spritesBG;
    public void Click()
    {
        index += 1;
        if (index >= spritesBG.Length) index = 0;
        image.sprite = spritesBG[index];
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
