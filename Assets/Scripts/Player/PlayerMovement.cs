using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float movSpeed;
    Vector2 direction;
    float movHorizontal;
    float movVertical;
    bool isMoving;
    Animator animator;
    pStatManager playerStat;

    [Header("Dash")]
    bool isDashing;
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    [SerializeField] bool isDashCooldown;

    [Header("Sound Effects")]
    [SerializeField] private SoundEffectDetailsSO soundEffectDetails; // Reference to the sound effects scriptable object
    private AudioSource movementAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerStat = GetComponent<pStatManager>();
        movementAudioSource = gameObject.AddComponent<AudioSource>();
        SetupMovementAudioSource();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimation();
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !isDashCooldown)
        {
            if (soundEffectDetails.playerDashSoundEffect != null)
            {
                SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.playerDashSoundEffect);
            }

            StartCoroutine(Dashing());
            StartCoroutine(DashCooldown());
        }
    }

    void Movement()
    {
        movHorizontal = Input.GetAxis("Horizontal");
        movVertical = Input.GetAxis("Vertical");
        direction = new Vector2(movHorizontal, movVertical);

        bool wasMoving = isMoving;
        isMoving = direction.magnitude > 0;

        if (isMoving && !wasMoving)
        {
            PlayMovementSound();
        }
        else if (!isMoving && wasMoving)
        {
            StopMovementSound();
        }

        if (isDashing)
        {
            body.velocity = direction.normalized * playerStat.stat.movSpd * dashSpeed;
            return;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            body.velocity = direction.normalized * playerStat.stat.movSpd * 0.4f;
        }
        else
        {
            body.velocity = direction.normalized * playerStat.stat.movSpd;
        }
    }

    void MoveAnimation()
    {
        if (movHorizontal == 0 && movVertical == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
    }

    private IEnumerator Dashing()
    {
        isDashing = true;
        playerStat.CallItemOnDash();
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    private IEnumerator DashCooldown()
    {
        isDashCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
    }

    private void SetupMovementAudioSource()
    {
        if (soundEffectDetails != null && soundEffectDetails.playerMovementSoundEffect != null)
        {
            movementAudioSource.clip = soundEffectDetails.playerMovementSoundEffect.soundEffectClip;
            movementAudioSource.volume = soundEffectDetails.playerMovementSoundEffect.soundEffectVolume;
            movementAudioSource.loop = true;
        }
    }

    private void PlayMovementSound()
    {
        if (soundEffectDetails != null && soundEffectDetails.playerMovementSoundEffect != null && !movementAudioSource.isPlaying)
        {
            float randomPitch = UnityEngine.Random.Range(soundEffectDetails.playerMovementSoundEffect.soundEffectPitchRandomVariationMin, soundEffectDetails.playerMovementSoundEffect.soundEffectPitchRandomVariationMax);
            movementAudioSource.pitch = randomPitch;
            movementAudioSource.Play();
        }
    }

    private void StopMovementSound()
    {
        if (movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
        }
    }
}
