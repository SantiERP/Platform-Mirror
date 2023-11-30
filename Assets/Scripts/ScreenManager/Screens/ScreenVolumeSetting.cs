using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ScreenVolumeSetting : MonoBehaviour, IScreen
{
    public AudioMixer _mixer;
    Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
        //ActivateButtons(false);
        //Time.timeScale = 0;
    }

    void ActivateButtons(bool enable)
    {
        foreach (var button in _buttons)
        {
            button.interactable = enable;
        }

    }

    public void Activate()
    {
        ActivateButtons(true);
    }

    public void Desactivate()
    {
        ActivateButtons(false);
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void SetMusicVolume(float sliderValue)
    {
        _mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        _mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

    public void BTN_Back()
    {
        ScreenManagerDefault.Instance.Pop();
    }

}
