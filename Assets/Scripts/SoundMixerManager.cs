using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundMixerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer audioMixer; 

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", level);
    }
    public void SetFXVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
    }
}
