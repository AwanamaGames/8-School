using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundEffectManager : SingletonMonobehaviour<SoundEffectManager>
{
    public int soundsVolume = 8;

    private void Start()
    {
        SetSoundsVolume(soundsVolume);
    }

    /// <summary>
    /// Play the sound effect
    /// </summary>
    public void PlaySoundEffect(SoundEffectSO soundEffect)
    {
        // Play sound using a sound gameobject and component from the object pool
        SoundEffect sound = (SoundEffect)PoolManager.Instance.ReuseComponent(soundEffect.soundPrefab, Vector3.zero, Quaternion.identity);

        sound.SetSound(soundEffect);
        sound.gameObject.SetActive(true);
        StartCoroutine(DisableSound(sound, soundEffect.soundEffectClip.length));
    }

    /// <summary>
    /// Disable sound effect object after it has played thus returning it to the object pool
    /// </summary>
    private IEnumerator DisableSound(SoundEffect sound, float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);
        sound.gameObject.SetActive(false);
    }

    /// <summary>
    /// Set sounds volume
    /// </summary>
    private void SetSoundsVolume(int soundsVolume)
    {
        float muteDecibels = -80f;

        if (SoundEffectResources.Instance == null)
        {
            Debug.LogError("SoundEffectResources.Instance is null");
            return;
        }

        if (SoundEffectResources.Instance.soundsMasterMixerGroup == null)
        {
            Debug.LogError("SoundEffectResources.Instance.soundsMasterMixerGroup is null");
            return;
        }

        if (SoundEffectResources.Instance.soundsMasterMixerGroup.audioMixer == null)
        {
            Debug.LogError("SoundEffectResources.Instance.soundsMasterMixerGroup.audioMixer is null");
            return;
        }

        if (soundsVolume == 0)
        {
            SoundEffectResources.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("SoundsVolume", muteDecibels);
        }
        else
        {
            SoundEffectResources.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", LinearToDecibels(soundsVolume));
        }
    }


    /// <summary>
    /// Convert he linear volume scale to decibels
    /// </summary>
    public float LinearToDecibels(int linear)
    {
        float linearScaleRange = 20f;

        // formula to convert from linear scale to the logarithmic decibel scale
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }
}