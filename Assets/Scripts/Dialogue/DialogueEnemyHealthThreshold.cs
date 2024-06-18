using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;

public class DialogueEnemyHealthThreshold : MonoBehaviour
{
    // UI References
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    // Reference to Gameplay UI Canvas to Hide during Dialogue
    [SerializeField] private GameObject gameplayUICanvas;

    // Dialogue Content
    [SerializeField] private string[] speaker;
    [SerializeField][TextArea] private string[] dialogueSentences;
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
    private StatManager enemyStat;

    // Health Threshold
    [SerializeField] private float healthThresholdPercent = 20f; // Percentage of health to trigger dialogue

    // Cinemachine Reference
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform cutsceneTarget;
    [SerializeField] private float zoomInOrthographicSize = 2;
    [SerializeField] private float zoomTransitionTime = .3f;
    [SerializeField] private float cameraMoveDamping = 1f;

    // Dialogue Control
    private bool dialogueActivated;
    private int currentStep;
    private Coroutine displayCoroutine;

    private Transform defaultFollowTarget;
    private Transform defaultLookAtTarget;
    private float defaultOrthographicSize;

    private void Start()
    {
        // Get the PlayerMovement and Rigidbody2D components from the player GameObject
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();

        // Get the NavMeshAgent and Animator components from the enemy GameObject
        enemyNavMeshAgent = enemy.GetComponent<NavMeshAgent>();
        enemyAnimator = enemy.GetComponent<Animator>();

        // Get the stat for enemy current hp calculation
        enemyStat = enemy.GetComponent<StatManager>();

        // Store the default camera settings
        defaultFollowTarget = virtualCamera.Follow;
        defaultLookAtTarget = virtualCamera.LookAt;
        defaultOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        if (!dialogueActivated)
        {
            CheckHealthAndTriggerDialogue();
        }

        if (dialogueActivated)
        {
            if (Input.GetButtonDown("Dialogue"))
            {
                if (displayCoroutine != null)
                {
                    StopCoroutine(displayCoroutine);
                    dialogueText.text = dialogueSentences[currentStep]; // Show full sentence
                }
                AdvanceDialogue();
            }
        }
    }

    private void CheckHealthAndTriggerDialogue()
    {
        if (enemyStat != null && enemyStat.stat.currentHP <= enemyStat.stat.maxHP * (healthThresholdPercent / 100f))
        {
            StartDialogue();
            Destroy(enemy); // Destroy enemy after starting the dialogue
        }
    }

    private void StartDialogue()
    {
        dialogueActivated = true;

        // Disable player movement
        originalPlayerVelocity = playerRigidbody.velocity;
        playerRigidbody.velocity = Vector2.zero;
        playerMovement.enabled = false;

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
        displayCoroutine = StartCoroutine(DisplayDialogueLetterByLetter(dialogueSentences[currentStep]));
    }

    private IEnumerator DisplayDialogueLetterByLetter(string sentence)
    {
        dialogueText.text = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text += sentence[i];
            yield return new WaitForSeconds(0.03f); // Adjust this value to control the speed of text display per letter
        }

        displayCoroutine = null; // Reset coroutine reference
    }

    private void AdvanceDialogue()
    {
        currentStep++;
        if (currentStep < speaker.Length)
        {
            speakerText.text = speaker[currentStep];
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
