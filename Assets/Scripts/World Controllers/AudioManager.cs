using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioSource LeftPlayer;
    public AudioSource RightPlayer ;
    
    public Sound[] sounds;
    public Dictionary<string, AudioSource> Kaart = new Dictionary<string, AudioSource>();

    // Start is called before the first frame update
    void Awake()
    {

        /*if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);*/
        
            foreach (Sound s in sounds)
        {
            //MusicLeftPlayer = gameObject.AddComponent<AudioSource>();
            //MusicRightPlayer = gameObject.AddComponent<AudioSource>();

            AudioSource NewLeft = Instantiate(LeftPlayer);
            AudioSource NewRight = Instantiate(RightPlayer);
            
            NewLeft.clip = s.clip;
            NewRight.clip = s.clip;
            
            NewLeft.volume = s.volume;
            NewRight.volume = s.volume;
            
            NewLeft.pitch = s.pitch;
            NewRight.pitch = s.pitch;
            
            NewLeft.loop = s.loop;
            NewRight.loop = s.loop;
            
            Kaart.Add(s.name + "1", NewLeft);
            Kaart.Add(s.name + "2", NewRight);
            
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) 
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        Kaart[s.name + "1"].Play();
        Kaart[s.name + "2"].Play();
        
    }
    
    public void StopPlaying (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        Kaart[s.name + "1"].Stop();
        Kaart[s.name + "2"].Stop();
    }

    public void StopMusic()
    {
        foreach (Sound s in sounds)
        {
            Kaart[s.name + "1"].Stop();
            Kaart[s.name + "2"].Stop();
        }
    }

    public AudioSource GetLeftMusic(string name) {return Kaart[name + "1"];}
    public AudioSource GetRightMusic(string name) {return Kaart[name + "2"];}
}
