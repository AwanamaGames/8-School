using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffectDetailsSO", menuName = "Scriptable Objects/Sounds/Sound Details")]
public class SoundEffectDetailsSO : ScriptableObject
{
    #region Header
    [Header("PLAYER SOUND EFFECT")]
    [Space(10)]
    #endregion
    #region Tooltip
    [Tooltip("The sound effect for player attack")]
    #endregion Tooltip
    public SoundEffectSO playerAttackSoundEffect;
    #region Tooltip
    [Tooltip("The sound effect for player movement")]
    #endregion Tooltip
    public SoundEffectSO playerMovementSoundEffect;
    #region Tooltip
    [Tooltip("The sound effect for player game over")]
    #endregion Tooltip
    public SoundEffectSO playerGameOverSoundEffect;
    #region Tooltip
    [Tooltip("The sound effect for player dash")]
    #endregion Tooltip
    public SoundEffectSO playerDashSoundEffect;

    #region Header
    [Header("INTERACTIVE SOUND EFFECT")]
    [Space(10)]
    #endregion
    #region Tooltip
    [Tooltip("The sound effect when interact with item")]
    #endregion Tooltip
    public SoundEffectSO itemGetSoundEffect;

    #region Tooltip
    [Tooltip("The sound effect when upgrade stat")]
    #endregion Tooltip
    public SoundEffectSO statUpgradeSoundEffect;

    #region Tooltip
    [Tooltip("The sound effect when leaf not enough")]
    #endregion Tooltip
    public SoundEffectSO notEnoughLeafSoundEffect;

    #region Tooltip
    [Tooltip("The sound effect when dialogue start")]
    #endregion Tooltip
    public SoundEffectSO gateSoundEffect;

    #region Tooltip
    [Tooltip("The sound effect when gate used")]
    #endregion Tooltip
    public SoundEffectSO dialogueSoundEffect;

    #region Tooltip
    [Tooltip("The sound effect when text is spread")]
    #endregion Tooltip
    public SoundEffectSO textSoundEffect;

    #region Header
    [Header("ENEMIES SOUND EFFECT")]
    [Space(10)]
    #endregion

    #region Header
    [Header("Pocong SOUND EFFECT")]
    [Space(10)]
    #endregion
    #region Tooltip
    [Tooltip("The sound effect for pocong attack")]
    #endregion Tooltip
    public SoundEffectSO pocongAttackSoundEffect;
    #region Tooltip
    [Tooltip("The sound effect for pocong defeat")]
    #endregion Tooltip
    public SoundEffectSO pocongDefeatedSoundEffect;

    #region Header
    [Header("Kunti SOUND EFFECT")]
    [Space(10)]
    #endregion
    #region Tooltip
    [Tooltip("The sound effect for kunti attack")]
    #endregion Tooltip
    public SoundEffectSO kuntiAttackSoundEffect;
    #region Tooltip
    [Tooltip("The sound effect for kunti defeat")]
    #endregion Tooltip
    public SoundEffectSO kuntiDefeatedSoundEffect;

    #region Header
    [Header("Tuyul SOUND EFFECT")]
    [Space(10)]
    #endregion
    #region Tooltip
    [Tooltip("The sound effect for tuyul attack")]
    #endregion Tooltip
    public SoundEffectSO tuyulAttackSoundEffect;
    #region Tooltip
    [Tooltip("The sound effect for tuyul defeat")]
    #endregion Tooltip
    public SoundEffectSO tuyulDefeatedSoundEffect;

    #region Header
    [Header("BOSS SOUND EFFECT")]
    [Space(10)]
    #endregion
    #region Tooltip
    [Tooltip("The sound effect for boss attack one")]
    #endregion Tooltip
    public SoundEffectSO bossAttackSoundEffectOne;
    #region Tooltip
    [Tooltip("The sound effect for boss attack two")]
    #endregion Tooltip
    public SoundEffectSO bossAttackSoundEffectTwo;
    #region Tooltip
    [Tooltip("The sound effect for boss defeat")]
    #endregion Tooltip
    public SoundEffectSO bossDefeatedSoundEffect;
}
