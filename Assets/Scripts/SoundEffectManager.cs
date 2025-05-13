using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    // Start is called before the first frame update
     public static SoundEffectManager Instance;
    [SerializeField] private AudioSource soundFXObject;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip AC,Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = AC;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength );
    }
}
