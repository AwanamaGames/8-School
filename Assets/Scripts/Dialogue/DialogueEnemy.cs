using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;

public class DialogueEnemy : MonoBehaviour
{
    // Sound Effect
    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

    // UI References
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    // Reference to Gameplay UI Canvas to Hide during Dialogue
    [SerializeField] private GameObject gameplayUICanvas;

    // Dialogue Content
    [SerializeField] private string[] speaker;
    [SerializeField][TextArea] private string[] dialogueSentences; // Each sentence will be displayed letter by letter
    [SerializeField] private Sprite[] portrait;

    // Cinematic Bar Reference
    [SerializeField] private CinematicBar cinematicBar;
    [SerializeField] private float cinematicBarSize = 600f;
    [SerializeField] private float transitionTime = .3f;

    // Player Reference
    [SerializeField] private GameObject player;
    private Rigidbody2D playerRigidbody;
    private PlayerMovement playerMovement;
    private Vector2 originalPlayerVelocity;

    // Enemy Reference
    [SerializeField] private GameObject enemy;
    private NavMeshAgent enemyNavMeshAgent;
    private Animator enemyAnimator;

    // Cinemachine Reference
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform cutsceneTarget; // Target to look at during the cutscene
    [SerializeField] private float zoomInOrthographicSize = 2;
    [SerializeField] private float zoomTransitionTime = .3f;
    [SerializeField] private float cameraMoveDamping = 1f; // Damping value for smooth camera movement

    // Dialogue Control
    private bool dialogueActivated;
    private int currentStep;
    private Coroutine displayCoroutine;
    private bool isDisplayingSentence;

    private Transform defaultFollowTarget;
    private Transform defaultLookAtTarget;
    private float defaultOrthographicSize;
    private AudioSource textSoundAudioSource;

    private void Start()
    {
        // Get the PlayerMovement and Rigidbody2D components from the player GameObject
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();

        // Get the NavMeshAgent and Animator components from the enemy GameObject
        enemyNavMeshAgent = enemy.GetComponent<NavMeshAgent>();
        enemyAnimator = enemy.GetComponent<Animator>();

        // Store the default camera settings
        defaultFollowTarget = virtualCamera.Follow;
        defaultLookAtTarget = virtualCamera.LookAt;
        defaultOrthographicSize = virtualCamera.m_Lens.OrthographicSize;

        // Initialize AudioSource for text sound effect
        textSoundAudioSource = gameObject.AddComponent<AudioSource>();
        textSoundAudioSource.loop = true; // Loop the sound for continuous playback
    }

    private void Update()
    {
        if (dialogueActivated)
        {
            if (Input.GetButtonDown("Dialogue"))
            {
                if (displayCoroutine != null)
                {
                    // Complete current letter-by-letter display immediately
                    StopCoroutine(displayCoroutine);
                    StopTextSound();
                    Debug.Log("Full text ahead");
                    dialogueText.text = dialogueSentences[currentStep]; // Show full sentence
                    displayCoroutine = null;
                    isDisplayingSentence = false;
                }
                else if (!isDisplayingSentence)
                {
                    // Move to the next dialogue step
                    AdvanceDialogue();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueActivated = true;

            // Disable player movement
            originalPlayerVelocity = playerRigidbody.velocity;
            playerRigidbody.velocity = Vector2.zero;
            playerMovement.enabled = false;

            // Stop the enemy
            enemyNavMeshAgent.isStopped = true;
            enemyAnimator.SetBool("isJumping", false);

            // Start the dialogue
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        if (soundEffectDetails.dialogueSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.dialogueSoundEffect);
        }

        // Hide Gameplay UI canvas
        if (gameplayUICanvas != null)
        {
            gameplayUICanvas.SetActive(false);
        }

        cinematicBar.Show(cinematicBarSize, transitionTime); // Show cinematic bars when dialogue starts
        dialogueCanvas.SetActive(true);

        // Switch camera target to the cutscene target
        virtualCamera.Follow = cutsceneTarget;
        virtualCamera.LookAt = cutsceneTarget;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = cameraMoveDamping;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = cameraMoveDamping;

        // Zoom in the camera
        StartCoroutine(ZoomCamera(zoomInOrthographicSize, zoomTransitionTime));

        // Initialize dialogue text display
        speakerText.text = speaker[currentStep];
        portraitImage.sprite = portrait[currentStep];
        displayCoroutine = StartCoroutine(DisplayDialogueLetterByLetter(dialogueSentences[currentStep]));
    }

    private IEnumerator DisplayDialogueLetterByLetter(string sentence)
    {
        PlayTextSound();

        isDisplayingSentence = true;
        dialogueText.text = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text += sentence[i];
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

    private void AdvanceDialogue()
    {
        currentStep++;
        if (currentStep < speaker.Length)
        {
            speakerText.text = speaker[currentStep];
            portraitImage.sprite = portrait[currentStep];
            displayCoroutine = StartCoroutine(DisplayDialogueLetterByLetter(dialogueSentences[currentStep]));
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
        cinematicBar.Hide(transitionTime); // Hide cinematic bars when dialogue ends
        playerMovement.enabled = true; // Enable player movement
        playerRigidbody.velocity = originalPlayerVelocity; // Restore the original velocity

        // Enable the enemy to move again
        enemyNavMeshAgent.isStopped = false;

        // Restore the camera to its default settings
        virtualCamera.Follow = defaultFollowTarget;
        virtualCamera.LookAt = defaultLookAtTarget;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = cameraMoveDamping;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = cameraMoveDamping;

        // Zoom out the camera
        StartCoroutine(ZoomOutAndDestroy(defaultOrthographicSize, zoomTransitionTime));

        // Show Gameplay UI canvas
        if (gameplayUICanvas != null)
        {
            gameplayUICanvas.SetActive(true);
        }
    }

    private IEnumerator ZoomOutAndDestroy(float targetSize, float duration)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;

        // Ensure the camera is at the target size before destroying the GameObject
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame
        Destroy(gameObject); // Destroy the dialogue GameObject
    }

    private IEnumerator ZoomCamera(float targetSize, float duration)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;
    }
}
