using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AudioController : Singleton<AudioController>
{
    /*public AudioSource MusicLeftPlayer;
    public AudioSource MusicRightPlayer;
    public AudioSource SoundEffectPlayerTemplate;
    public List<AudioSource> SoundEffectPlayers;

    public List<AudioClip> tracks;
    public List<int> trackWeights;

    private void Awake()
    {
        SoundEffectPlayers = new List<AudioSource>();
        TimeUnitChange.timeChangeEvent += Passing;

        if (tracks.Count != trackWeights.Count)
        {
            Debug.LogError("If you add or remove track from the track list, you must also add or remove the track's weight from the weight lest ");
        }
    }

    private void Passing(int currentMoment)
    {
        if (currentMoment % 4 == 0)
        {
            if (!MusicLeftPlayer.isPlaying && !MusicRightPlayer.isPlaying)
            {
                LotteryNextSoundtrack();
            }
            
            //Garbage Disposal
            List<AudioSource> Garbage = new List<AudioSource>();
            foreach (AudioSource SoundEffectPlayer in SoundEffectPlayers)
            {
                if (!SoundEffectPlayer.isPlaying)
                {
                    Garbage.Add(SoundEffectPlayer);
                }
            }

            foreach (AudioSource SoundEffectPlayer in Garbage)
            {
                SoundEffectPlayers.Remove(SoundEffectPlayer);
            }
        }
    }

    public void LotteryNextSoundtrack()
    {
        AudioClip nextTrack = null;
        
        int totalWeight = 0;
        foreach (int weight in trackWeights)
        {
            totalWeight += weight;
        }

        int trackNumber = Random.Range(0, totalWeight);
        
        int weightCounter = 0;
        for (int i = 0; i < tracks.Count; i++)
        {
            weightCounter += trackWeights[i];
            if (weightCounter >= trackNumber) nextTrack = tracks[i];
        }

        if (nextTrack != null)
        {
            MusicLeftPlayer.clip = nextTrack;
            MusicRightPlayer.clip = nextTrack;
            MusicLeftPlayer.Play();
            MusicRightPlayer.Play();
        }
    }

    public void StopSoundTrack()
    {
        MusicLeftPlayer.Stop();
        MusicRightPlayer.Stop();
    }

    public void PlaySoundEffect(AudioClip SoundEffect)
    {
        AudioSource SoundEffectPlayer = Instantiate(SoundEffectPlayerTemplate);
        SoundEffectPlayer.clip = SoundEffect;
        SoundEffectPlayer.Play();
        SoundEffectPlayers.Add(SoundEffectPlayer);
    }*/
}
