using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocongAttackManager : MonoBehaviour, IAttack
{
    public bool agro = false;
    Animator animator;
    GameObject player;
    public bool isAttacking;

    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void update(){
    }

    void attack(){

        if (soundEffectDetails.pocongAttackSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.pocongAttackSoundEffect);
        }

        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        animator.Play("Attack");
    }

    void jumpAttack()
    {
    }
    public void triggered()
    {
        if (isAttacking == false)
        {
        attack();
        }
    }

    void animationEventEndAttacking()
    {
        animator.Play("Idle");
    }
}
