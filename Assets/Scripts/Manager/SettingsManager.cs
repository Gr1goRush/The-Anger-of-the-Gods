using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static int music;
    public static int effect;
    public static int vibro;
    public static int volume;
    public CustomTogle customTogleMusic;
    public CustomTogle customTogleEffect;
    public CustomTogle customTogleVibro;
    public static void Load()
    {
        music = PlayerPrefs.GetInt("music");
        effect = PlayerPrefs.GetInt("effect");
        vibro = PlayerPrefs.GetInt("vibro");
        volume = PlayerPrefs.GetInt("volume");
        AudioMixerGroup audioMixer = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<AudioSource>().outputAudioMixerGroup;
        if (music == 0) audioMixer.audioMixer.SetFloat("MusicVolume", -80);
        else audioMixer.audioMixer.SetFloat("MusicVolume", 0);
        if (effect == 0) audioMixer.audioMixer.SetFloat("effectVolume", -80);
        else audioMixer.audioMixer.SetFloat("effectVolume", 0);
        audioMixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 10, ((float)volume / 10)));
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();

        customTogleMusic.Set(IntToBool(music));
        customTogleEffect.Set(IntToBool(effect));
        customTogleVibro.Set(IntToBool(vibro));
    }

    bool IntToBool(int Int)
    {
        if(Int > 0)
        {
            return true;
        }
        return false;
    }

    public void ToggleMusic(bool isOn)
    {
        music = isOn ? 1:0;
        PlayerPrefs.SetInt("music", music);
        Load();
    }
    public void ToggleEffect(bool isOn)
    {
        effect = isOn ? 1 : 0;
        PlayerPrefs.SetInt("effect", effect);
        Load();
    }
    public void ToggleVibro(bool isOn)
    {
        vibro = isOn ? 1 : 0;
        PlayerPrefs.SetInt("vibro", vibro);
        Load();
    }

    public void Plus()
    {
        volume += 1;
        volume = Mathf.Clamp(volume, 0, 10);
        PlayerPrefs.SetInt("volume", volume);
        Load();
    }
    public void Minus()
    {
        volume -= 1;
        volume = Mathf.Clamp(volume, 0, 10);
        PlayerPrefs.SetInt("volume", volume);
        Load();
    }
    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
