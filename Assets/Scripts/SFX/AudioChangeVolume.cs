using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam = "Music_Volume";
    public void ChangeValue(float sliderValue)
    {
        group.SetFloat(floatParam, sliderValue);
    }
}
