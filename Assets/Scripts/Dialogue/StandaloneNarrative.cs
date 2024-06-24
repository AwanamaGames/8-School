using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum NarrativeType
{
    IntroScene,
    EndScene
}

public class StandaloneNarrative : MonoBehaviour
{
    // UI References
    [SerializeField] private GameObject narrativeCanvas;
    [SerializeField] private TMP_Text narrativeText;
    [SerializeField] private Image fullScreenImage;

    // Narrative Content
    [SerializeField][TextArea] private string[] narrativeSentences;
    [SerializeField] private Sprite[] images;

    // Fade Transition
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    // Sound Effect
    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;
    private AudioSource textSoundAudioSource;

    // Narrative Type
    [SerializeField] private NarrativeType narrativeType;

    // Narrative Control
    private int currentStep;
    private Coroutine displayCoroutine;
    private bool isDisplayingSentence;

    private void Start()
    {
        // Initialize AudioSource for text sound effect
        textSoundAudioSource = gameObject.AddComponent<AudioSource>();
        textSoundAudioSource.loop = true; // Loop the sound for continuous playback

        StartCoroutine(FadeInAndStartNarrative());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Dialogue"))
        {
            if (displayCoroutine != null)
            {
                // Complete current letter-by-letter display immediately
                StopCoroutine(displayCoroutine);
                StopTextSound();
                narrativeText.text = narrativeSentences[currentStep]; // Show full sentence
                displayCoroutine = null;
                isDisplayingSentence = false;
            }
            else if (!isDisplayingSentence)
            {
                // Move to the next narrative step
                AdvanceNarrative();
            }
        }
    }

    private IEnumerator FadeInAndStartNarrative()
    {
        // Fade in the narrative canvas
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Start displaying the narrative
        StartNarrative();
    }

    private void StartNarrative()
    {
        narrativeCanvas.SetActive(true);
        currentStep = 0;
        ShowCurrentStep();
    }

    private void ShowCurrentStep()
    {
        if (currentStep < images.Length)
        {
            fullScreenImage.sprite = images[currentStep];
        }
        else
        {
            fullScreenImage.sprite = null;
        }

        displayCoroutine = StartCoroutine(DisplayNarrativeLetterByLetter(narrativeSentences[currentStep]));
    }

    private IEnumerator DisplayNarrativeLetterByLetter(string sentence)
    {
        PlayTextSound();

        isDisplayingSentence = true;
        narrativeText.text = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            narrativeText.text += sentence[i];
            yield return new WaitForSeconds(0.01f); // Adjust this value to control the speed of text display per letter
        }

        displayCoroutine = null; // Reset coroutine reference
        isDisplayingSentence = false;

        StopTextSound();
    }

    private void PlayTextSound()
    {
        if (soundEffectDetails.textSoundEffect != null)
        {
            textSoundAudioSource.clip = soundEffectDetails.textSoundEffect.soundEffectClip;
            textSoundAudioSource.volume = soundEffectDetails.textSoundEffect.soundEffectVolume;
            textSoundAudioSource.pitch = Random.Range(soundEffectDetails.textSoundEffect.soundEffectPitchRandomVariationMin, soundEffectDetails.textSoundEffect.soundEffectPitchRandomVariationMax);
            textSoundAudioSource.Play();
        }
    }

    private void StopTextSound()
    {
        textSoundAudioSource.Stop();
    }

    private void AdvanceNarrative()
    {
        currentStep++;
        if (currentStep < narrativeSentences.Length)
        {
            ShowCurrentStep();
        }
        else
        {
            StartCoroutine(EndNarrativeAndLoadNextScene());
        }
    }

    private IEnumerator EndNarrativeAndLoadNextScene()
    {
        // Fade out the narrative canvas
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // Determine next scene based on narrativeType
        if (narrativeType == NarrativeType.IntroScene)
        {
            // Notify GameManager to load the tutorial scene
            GameManager.Instance.ChangeState(GameState.Tutorial);
        }
        else if (narrativeType == NarrativeType.EndScene)
        {
            // Notify GameManager to load the main menu scene
            GameManager.Instance.ChangeState(GameState.MainMenu);
        }
    }
}
