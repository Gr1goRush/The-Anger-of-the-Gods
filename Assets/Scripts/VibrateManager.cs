using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateManager : MonoBehaviour
{
    bool isVibrate;
    bool isAllow;
    private void Start()
    {
        if (PlayerPrefs.GetInt("vibro") == 1)
        {
            isAllow = true;
        }
        else
        {
            isAllow = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isVibrate && isAllow){
            Handheld.Vibrate();
        }
    }
    public void PlayVibration(float time)
    {
        StartCoroutine(VibrationTime(time));
    }
    public void PlayVibrate()
    {
        if(isAllow)
            Handheld.Vibrate();
    }
    IEnumerator VibrationTime(float time)
    {
        isVibrate = true;
        yield return new WaitForSeconds(time);
        isVibrate = false;
    }
}
