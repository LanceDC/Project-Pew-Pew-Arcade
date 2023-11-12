using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;
    private int currentSongPlayingIndex = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //Sets up the sounds that can be played
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play(sounds[0].name);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!sounds[currentSongPlayingIndex].source.isPlaying)
        {
            currentSongPlayingIndex++;

            if (currentSongPlayingIndex == sounds.Length)
                currentSongPlayingIndex = 0;

            Play(sounds[currentSongPlayingIndex].name);
        }
    }

    public void Play(string name)
    {
        //Looks for the sound that matches the name and returns it
        //sound is the class that matches the song name and so returns it
        Sounds s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.Log("Sound " + s.name + " was not found");
            return;
        }

        s.source.Play();

    }
}
