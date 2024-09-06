using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public RuntimeAnimatorController[] controllers;
    // Start is called before the first frame update
    void Start()
    {
        int select = PlayerPrefs.GetInt("skinSelect");
        GetComponent<Animator>().runtimeAnimatorController = controllers[select];
    }
}
