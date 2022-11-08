using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resDropdown;
    Resolution[] resolution;

    private void Start()
    {
        resolution = Screen.resolutions;
        int currentRes = 0;

        List<string> resOptions = new List<string>();

        for (int i = 0; i < resolution.Length; i++)
        {
            string res = resolution[i].width + "x" + resolution[i].height;
            resOptions.Add(res);

            if (resolution[i].width == Screen.currentResolution.width && resolution[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resDropdown.ClearOptions();
        resDropdown.AddOptions(resOptions);
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolutions = resolution[index];
        Screen.SetResolution(resolutions.width, resolutions.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
