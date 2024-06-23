using UnityEngine;
using UnityEngine.Audio;

public class MusicResources : MonoBehaviour
{
    private static MusicResources instance;

    public static MusicResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<MusicResources>("MusicResources");
            }
            return instance;
        }
    }

    [Header("AUDIO MIXER GROUPS")]
    [Tooltip("Populate with music master mixer group")]
    public AudioMixerGroup musicMasterMixerGroup;

    [Header("AUDIO SNAPSHOTS")]
    [Tooltip("Populate with music off snapshot")]
    public AudioMixerSnapshot musicOffSnapshot;

    [Tooltip("Populate with music on full snapshot")]
    public AudioMixerSnapshot musicOnFullSnapshot;

    [Tooltip("Populate with music low snapshot")]
    public AudioMixerSnapshot musicLowSnapshot;
}
