using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour {

    public int enemyType;

    public AudioSource[] audioClips;

    public AudioSource[] audioClipsHit;

    void Start () {
    }
	
	void Update () {
		if (isFading == true) {
            float lerp = ((Time.time - startTime) / fadeTime);
            if (lerp > 1) {
                sourcetofade.volume = origVol;
                isFading = false;
                sourcetofade.Stop();
            }
            else {
                sourcetofade.volume = Mathf.Lerp(origVol, 0, lerp);
            }
        }
        if (isinFading == true)
        {
            float lerpin = ((Time.time - startinTime) / fadeinTime);
            if (lerpin > 1)
            {
                sourcetofadein.volume = originVol;
                isinFading = false;
            }
            else
            {
                sourcetofadein.volume = Mathf.Lerp(0, originVol, lerpin);
            }
        }
    }

    public enum MusicMood
    {
        None = 0,
        MainMenu = 17,
        BridgeSection = 18,
        WorldOpenUp = 19,
        NorthernPart = 20,
        Nightmare = 21,
        Ascend = 22
    };

    MusicMood currentlyPlayingMusic = MusicMood.None;

    public void MusicManager(MusicMood mood)
    {
        if (currentlyPlayingMusic != mood)
        {
            if (mood == MusicMood.MainMenu)
            {
                PlaySound((int)mood);
            }
            else
            {
                if (currentlyPlayingMusic != MusicMood.None)
                {
                    FadeSound((int)currentlyPlayingMusic);
                }
                if (mood != MusicMood.None)
                {
                    FadeInSound((int)mood);
                }
            }
            currentlyPlayingMusic = mood;
        }
    }

    public void PlaySound (int arrayIndex) {
        if (enemyType == 1 && arrayIndex == 1 && audioClips[1].isPlaying == true) {
            audioClips[2].Play();
        }
        else if (enemyType == 8 && arrayIndex == 0 && audioClips[0].isPlaying == true) {
            if (audioClips[1].isPlaying == true) {
                audioClips[6].Play();
            }
            else {
                audioClips[1].Play();
            }
        }
        else if (audioClips[arrayIndex].isPlaying == false) {
            audioClips[arrayIndex].Play();
        }
    }

    public void StopSound(int arrayIndex) {
        if (audioClips[arrayIndex].isPlaying == true) {
            audioClips[arrayIndex].Stop();
        }
    }

    public float fadeTime;
    float origVol;
    float startTime;
    bool isFading = false;
    AudioSource sourcetofade;

    public float fadeinTime;
    float originVol;
    float startinTime;
    bool isinFading = false;
    AudioSource sourcetofadein;

    public void FadeSound(int arrayIndex) {
        if (audioClips[arrayIndex].isPlaying == true) {
            sourcetofade = audioClips[arrayIndex];
            origVol = audioClips[arrayIndex].volume;
            startTime = Time.time;
            isFading = true;
        }
    }

    public void FadeInSound(int arrayIndex)
    {
        if (audioClips[arrayIndex].isPlaying == false)
        {
            sourcetofadein = audioClips[arrayIndex];
            originVol = audioClips[arrayIndex].volume;
            startinTime = Time.time;
            isinFading = true;
            sourcetofadein.Play();
        }
    }

    public void PlaySoundHit(int arrayIndex) {
        if (audioClipsHit[arrayIndex].isPlaying == false) {
            audioClipsHit[arrayIndex].Play();
        }
    }

    public void StopSoundHit(int arrayIndex) {
        if (audioClipsHit[arrayIndex].isPlaying == true) {
            audioClipsHit[arrayIndex].Stop();
        }
    }
}
