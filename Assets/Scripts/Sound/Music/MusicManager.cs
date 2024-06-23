using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MusicManager : SingletonMonobehaviour<MusicManager>
{
    public List<MusicTrackSO> musicTracks;
    private AudioSource musicAudioSource = null;
    private Coroutine fadeOutMusicCoroutine;
    private Coroutine fadeInMusicCoroutine;
    public int musicVolume = 10;
    private MusicTrackSO currentSceneMusicTrack;
    private MusicTrackSO bossMusicTrack;

    protected override void Awake()
    {
        base.Awake();

        musicAudioSource = GetComponent<AudioSource>();
        if (musicAudioSource == null)
        {
            musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // Start with music off
        MusicResources.Instance.musicOffSnapshot.TransitionTo(0f);

        musicAudioSource.loop = true;  // Ensure the music loops
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetInt("musicVolume");
        }
        SetMusicVolume(musicVolume);

        StartCoroutine(CheckActiveScenePeriodically());
    }

    private IEnumerator CheckActiveScenePeriodically()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1f); // Adjust polling frequency as needed

        while (true)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            currentSceneMusicTrack = GetMusicTrackForScene(currentScene.name);

            if (bossMusicTrack == null && currentSceneMusicTrack != null && musicAudioSource.clip != currentSceneMusicTrack.musicClip)
            {
                PlayMusic(currentSceneMusicTrack);
                Debug.Log("Music Track Detected for scene: " + currentScene.name);
            }

            yield return waitTime;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("musicVolume", musicVolume);
    }

    public void PlayMusic(MusicTrackSO musicTrack, float fadeOutTime = 1f, float fadeInTime = 1f)
    {
        StartCoroutine(PlayMusicRoutine(musicTrack, fadeOutTime, fadeInTime));
    }

    private IEnumerator PlayMusicRoutine(MusicTrackSO musicTrack, float fadeOutTime, float fadeInTime)
    {
        if (fadeOutMusicCoroutine != null)
        {
            StopCoroutine(fadeOutMusicCoroutine);
        }

        if (fadeInMusicCoroutine != null)
        {
            StopCoroutine(fadeInMusicCoroutine);
        }

        yield return fadeOutMusicCoroutine = StartCoroutine(FadeOutMusic(fadeOutTime));

        musicAudioSource.clip = musicTrack.musicClip;
        musicAudioSource.volume = 0f;
        musicAudioSource.Play();

        yield return fadeInMusicCoroutine = StartCoroutine(FadeInMusic(musicTrack, fadeInTime));
    }

    private IEnumerator FadeOutMusic(float fadeOutTime)
    {
        MusicResources.Instance.musicLowSnapshot.TransitionTo(fadeOutTime);

        yield return new WaitForSeconds(fadeOutTime);
    }

    private IEnumerator FadeInMusic(MusicTrackSO musicTrack, float fadeInTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInTime)
        {
            musicAudioSource.volume = Mathf.Lerp(0f, musicTrack.musicVolume, elapsedTime / fadeInTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicAudioSource.volume = musicTrack.musicVolume;
        MusicResources.Instance.musicOnFullSnapshot.TransitionTo(fadeInTime);
    }

    public void IncreaseMusicVolume()
    {
        int maxMusicVolume = 20;

        if (musicVolume >= maxMusicVolume) return;

        musicVolume += 1;
        SetMusicVolume(musicVolume);
    }

    public void DecreaseMusicVolume()
    {
        if (musicVolume == 0) return;

        musicVolume -= 1;
        SetMusicVolume(musicVolume);
    }

    public void SetMusicVolume(int musicVolume)
    {
        float muteDecibels = -80f;

        if (MusicResources.Instance == null)
        {
            Debug.LogError("MusicResources.Instance is null");
            return;
        }

        if (MusicResources.Instance.musicMasterMixerGroup == null)
        {
            Debug.LogError("MusicResources.Instance.musicMasterMixerGroup is null");
            return;
        }

        if (MusicResources.Instance.musicMasterMixerGroup.audioMixer == null)
        {
            Debug.LogError("MusicResources.Instance.musicMasterMixerGroup.audioMixer is null");
            return;
        }

        if (musicVolume == 0)
        {
            MusicResources.Instance.musicMasterMixerGroup.audioMixer.SetFloat("musicVolume", muteDecibels);
        }
        else
        {
            MusicResources.Instance.musicMasterMixerGroup.audioMixer.SetFloat("musicVolume", LinearToDecibels(musicVolume));
        }

        musicAudioSource.volume = musicVolume / 20f;  // Adjust volume based on linear scale
    }

    private float LinearToDecibels(int linear)
    {
        float linearScaleRange = 20f;
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }

    public MusicTrackSO GetMusicTrackForScene(string sceneName)
    {
        foreach (var track in musicTracks)
        {
            if (track.sceneName == sceneName)
            {
                return track;
            }
        }
        return null;
    }

    public void EnterBossRoom(MusicTrackSO bossMusic)
    {
        bossMusicTrack = bossMusic;
        PlayMusic(bossMusicTrack);
    }

    public void ExitBossRoom()
    {
        bossMusicTrack = null;
        if (currentSceneMusicTrack != null)
        {
            PlayMusic(currentSceneMusicTrack);
        }
    }
}
