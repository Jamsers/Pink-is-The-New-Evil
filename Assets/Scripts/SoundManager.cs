using UnityEngine;

public class SoundManager : MonoBehaviour {

    public int enemyType;

    public AudioSource[] audioClips;

    public AudioSource[] audioClipsHit;

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

    public enum MusicMood {
        None = 0,
        MainMenu = 17,
        BridgeSection = 18,
        WorldOpenUp = 19,
        NorthernPart = 20,
        Nightmare = 21,
        Ascend = 22
    };

    MusicMood currentlyPlayingMusic = MusicMood.None;

    public void AllowMusicToPlayWhilePaused(bool can) {
        audioClips[(int)MusicMood.MainMenu].ignoreListenerPause = can;
        audioClips[(int)MusicMood.BridgeSection].ignoreListenerPause = can;
        audioClips[(int)MusicMood.WorldOpenUp].ignoreListenerPause = can;
        audioClips[(int)MusicMood.NorthernPart].ignoreListenerPause = can;
        audioClips[(int)MusicMood.Nightmare].ignoreListenerPause = can;
        audioClips[(int)MusicMood.Ascend].ignoreListenerPause = can;

        audioClips[(int)MusicMood.MainMenu].ignoreListenerVolume = can;
        audioClips[(int)MusicMood.BridgeSection].ignoreListenerVolume = can;
        audioClips[(int)MusicMood.WorldOpenUp].ignoreListenerVolume = can;
        audioClips[(int)MusicMood.NorthernPart].ignoreListenerVolume = can;
        audioClips[(int)MusicMood.Nightmare].ignoreListenerVolume = can;
        audioClips[(int)MusicMood.Ascend].ignoreListenerVolume = can;
    }

    void Start() {
        if (enemyType == 8)
            AllowMusicToPlayWhilePaused(true);
    }

    void Update() {
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
        if (isinFading == true) {
            float lerpin = ((Time.time - startinTime) / fadeinTime);
            if (lerpin > 1) {
                sourcetofadein.volume = originVol;
                isinFading = false;
            }
            else {
                sourcetofadein.volume = Mathf.Lerp(0, originVol, lerpin);
            }
        }
    }

    public void MusicManager(MusicMood mood) {
        if (currentlyPlayingMusic != mood) {
            if (mood == MusicMood.MainMenu) {
                PlaySound((int)mood);
            }
            else {
                if (mood == MusicMood.Ascend) {
                    fadeTime = 0.5f;
                    fadeinTime = 0.5f;
                }

                if (currentlyPlayingMusic != MusicMood.None) {
                    FadeSound((int)currentlyPlayingMusic);
                }
                if (mood != MusicMood.None) {
                    FadeInSound((int)mood);
                }
            }
            currentlyPlayingMusic = mood;
        }
    }

    public void PlaySound(int arrayIndex) {
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



    public void FadeSound(int arrayIndex) {
        if (audioClips[arrayIndex].isPlaying == true) {
            sourcetofade = audioClips[arrayIndex];
            origVol = audioClips[arrayIndex].volume;
            startTime = Time.time;
            isFading = true;
        }
    }

    public void FadeInSound(int arrayIndex) {
        if (audioClips[arrayIndex].isPlaying == false) {
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
