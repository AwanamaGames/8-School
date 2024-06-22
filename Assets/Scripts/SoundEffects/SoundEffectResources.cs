using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectResources : MonoBehaviour
{
    private static SoundEffectResources instance;

    public static SoundEffectResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<SoundEffectResources>("SoundEffectResources");
            }
            return instance;
        }
    }

    #region Header SOUNDS
    [Space(10)]
    [Header("SOUNDS")]
    #endregion Header

    #region Tooltip
    [Tooltip("Populate with sounds master mixer group")]
    #endregion Tooltip
    public AudioMixerGroup soundsMasterMixerGroup;

    //#region Tooltip
    //[Tooltip("Door open close sound effect")]
    //#endregion Tooltip
    //public SoundEffectSO doorOpenCloseSoundEffect;
}
