using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private static AudioSystem instance;
    public static AudioSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioSystem>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AudioManager";
                    instance = obj.AddComponent<AudioSystem>();
                }
            }
            return instance;
        }
    }
    [SerializeField]
    private GameObject audioSourceObject;
    public int maxSimultaneousSounds = 5; // Maximum number of sounds that can be played simultaneously
    private AudioSource[] audioSources;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize the AudioSource
        if(audioSources == null) {
            audioSources = new AudioSource[maxSimultaneousSounds];
            for (int i = 0; i < maxSimultaneousSounds; i++)
            {
                GameObject objectInstance = Instantiate(audioSourceObject, transform.position, Quaternion.identity);
                audioSources[i] = objectInstance.GetComponent<AudioSource>();
                objectInstance.transform.parent = this.transform;
            }
        }
    }

    private void OnDestroy()
    {
        ClearAudioObjects();
    }

    public void PlaySound(AudioClip clip, float volume, bool isLoop = false)
    {
        // Find an available AudioSource to play the sound
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = volume;
                if(isLoop)
                    source.loop = true;
                else
                    source.loop = false;
                source.Play();
                return;
            }
        }
    }

    public void ClearAudioObjects() {
        foreach(AudioSource source in audioSources) {
            Destroy(source.gameObject);
        }
        audioSources = null;
    }
}

// To call the function in another script
// AudioSystem.Instance.PlaySound(AudioClip clip, float volume, bool isLoop = false or true)
