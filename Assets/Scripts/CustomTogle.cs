using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class CustomTogle : MonoBehaviour
{
    public Image image;
    public Sprite On;
    public Sprite Of;
    public bool isOn = true;
    public UnityEvent<bool> Event =new UnityEvent<bool>();
    public void Set(bool isOn)
    {
        this.isOn = isOn;
        if (isOn)
        {
            image.sprite = On;
        }
        else
        {
            image.sprite = Of;
        }
    }
    public void Click()
    {
        isOn = !isOn;
        if (isOn)
        {
            image.sprite = On;
        }
        else
        {
            image.sprite = Of;
        }
        Event.Invoke(isOn);
    }
}
