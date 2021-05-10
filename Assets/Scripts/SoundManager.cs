using UnityEngine;

public class SoundManager : MonoBehaviour {
    public CharacterType characterType;
    public AudioSource[] audioClips;
    public AudioSource[] audioClipsHit;

    float fadeTime = 2f;
    float musicFadingOutOriginalVolume;
    float musicFadingOutStartTime;
    bool isMusicFadingOut = false;
    AudioSource musicToFadeOut;
    float musicFadingInOriginalVolume;
    float musicFadingInStartTime;
    bool isMusicFadingIn = false;
    AudioSource musicToFadeIn;

    MusicMood currentlyPlayingMusic = MusicMood.None;

    public enum CharacterType {
        Player,
        Enemy1,
        Enemy2,
        Enemy3,
        Enemy4,
        Enemy5,
        Enemy6,
        Enemy7
    }

    public enum MusicMood {
        None = 0,
        MainMenu = 17,
        BridgeSection = 18,
        WorldOpenUp = 19,
        NorthernPart = 20,
        Nightmare = 21,
        Ascend = 22
    };

    void Start() {
        if (characterType == CharacterType.Player)
            AllowMusicToPlayWhilePaused(true);
    }

    void Update() {
        if (isMusicFadingOut == true) {
            float lerp = ((Time.time - musicFadingOutStartTime) / fadeTime);
            if (lerp > 1) {
                musicToFadeOut.Stop();
                musicToFadeOut.volume = musicFadingOutOriginalVolume;
                isMusicFadingOut = false;
            }
            else {
                musicToFadeOut.volume = Mathf.Lerp(musicFadingOutOriginalVolume, 0, lerp);
            }
        }

        if (isMusicFadingIn == true) {
            float lerp = ((Time.time - musicFadingInStartTime) / fadeTime);
            if (lerp > 1) {
                musicToFadeIn.volume = musicFadingInOriginalVolume;
                isMusicFadingIn = false;
            }
            else {
                musicToFadeIn.volume = Mathf.Lerp(0, musicFadingInOriginalVolume, lerp);
            }
        }
    }

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

    public void MusicManager(MusicMood mood) {
        if (currentlyPlayingMusic == mood)
            return;

        if (mood == MusicMood.MainMenu) {
            PlaySound((int)mood);
        }
        else {
            if (mood == MusicMood.Ascend)
                fadeTime = 0.5f;

            if (currentlyPlayingMusic != MusicMood.None)
                FadeOutMusic((int)currentlyPlayingMusic);

            if (mood != MusicMood.None)
                FadeInMusic((int)mood);
        }

        currentlyPlayingMusic = mood;
    }

    public void PlaySound(int arrayIndex) {
        // hacky overlapping audio logic
        if (characterType == CharacterType.Enemy1 && arrayIndex == 1 && audioClips[arrayIndex].isPlaying == true) {
            audioClips[2].Play();
        }
        else if (characterType == CharacterType.Player && arrayIndex == 0 && audioClips[arrayIndex].isPlaying == true) {
            if (audioClips[1].isPlaying == true) {
                audioClips[6].Play();
            }
            else {
                audioClips[1].Play();
            }
        }

        if (audioClips[arrayIndex].isPlaying == false)
            audioClips[arrayIndex].Play();
    }

    public void StopSound(int arrayIndex) {
        if (audioClips[arrayIndex].isPlaying == true)
            audioClips[arrayIndex].Stop();
    }

    public void PlaySoundHit(int arrayIndex) {
        if (audioClipsHit[arrayIndex].isPlaying == false)
            audioClipsHit[arrayIndex].Play();
    }

    public void StopSoundHit(int arrayIndex) {
        if (audioClipsHit[arrayIndex].isPlaying == true)
            audioClipsHit[arrayIndex].Stop();
    }

    public void FadeOutMusic(int arrayIndex) {
        if (audioClips[arrayIndex].isPlaying == false)
            return;

        musicToFadeOut = audioClips[arrayIndex];
        musicFadingOutOriginalVolume = audioClips[arrayIndex].volume;
        musicFadingOutStartTime = Time.time;
        isMusicFadingOut = true;
    }

    public void FadeInMusic(int arrayIndex) {
        if (audioClips[arrayIndex].isPlaying == true)
            return;

        musicToFadeIn = audioClips[arrayIndex];
        musicFadingInOriginalVolume = audioClips[arrayIndex].volume;
        musicFadingInStartTime = Time.time;
        isMusicFadingIn = true;
        musicToFadeIn.Play();
    }
}