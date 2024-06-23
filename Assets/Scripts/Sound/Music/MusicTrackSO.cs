using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrack_", menuName = "Scriptable Objects/Sounds/MusicTrack")]
public class MusicTrackSO : ScriptableObject
{
    [Header("MUSIC TRACK DETAILS")]
    [Tooltip("The name for the music track")]
    public string musicName;

    [Tooltip("The audio clip for the music track")]
    public AudioClip musicClip;

    [Tooltip("The volume for the music track")]
    [Range(0f, 1f)]
    public float musicVolume = 1f;

    [Tooltip("The scene name associated with this music track")]
    public string sceneName;
}
