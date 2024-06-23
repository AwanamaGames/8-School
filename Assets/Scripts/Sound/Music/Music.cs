using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
public class Music : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;  // Ensure the music loops
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the scene has a specified music track and play it
        MusicTrackSO musicTrack = MusicManager.Instance.GetMusicTrackForScene(scene.name);
        if (musicTrack != null)
        {
            SetMusic(musicTrack);
        }
    }

    /// <summary>
    /// Set the music to play
    /// </summary>
    public void SetMusic(MusicTrackSO musicTrack)
    {
        audioSource.clip = musicTrack.musicClip;
        audioSource.volume = musicTrack.musicVolume;
        audioSource.Play();
    }
}
