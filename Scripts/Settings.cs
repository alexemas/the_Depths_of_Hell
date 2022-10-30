using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    private bool isFullScreen;
    public AudioMixer am;
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }

    public void Quality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
