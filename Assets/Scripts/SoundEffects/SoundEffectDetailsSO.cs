using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffectDetailsSO", menuName = "Scriptable Objects/Sounds/Sound Details")]
public class SoundEffectDetailsSO : ScriptableObject
{
    #region Tooltip
    [Tooltip("The sound effect for player attack")]
    #endregion Tooltip
    public SoundEffectSO playerAttackSoundEffect;

    #region Tooltip
    [Tooltip("The sound effect when interact with item")]
    #endregion Tooltip
    public SoundEffectSO itemGetSoundEffect;

    #region Tooltip
    [Tooltip("The sound effect when upgrade stat")]
    #endregion Tooltip
    public SoundEffectSO statUpgradeSoundEffect;

    //#region Tooltip
    //[Tooltip("The sound effect for pocong attack")]
    //#endregion Tooltip
    //public SoundEffectSO pocongAttack;
}
