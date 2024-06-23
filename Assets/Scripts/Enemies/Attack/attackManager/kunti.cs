using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class kuntiAttackManager : MonoBehaviour, IAttack
{
    public bool isAgro;
    public NavMeshAgent agent;
    GameObject player;
    private int index = 0;
    private bool rangeAttackIsCooldown = false;
    private bool meleeAttakIsCooldown = false;
    StatManager statManager;

    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

    Animator animator;
    public bool isAttacking;
    
    async void Start()
    {   
        statManager = GetComponent<StatManager>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        await getStat();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (statManager.isAgro == true && agent.velocity.x == 0 && agent.velocity.y == 0 && rangeAttackIsCooldown == false && isAttacking == false && statManager.inRange == false)
        {
            Debug.Log("test");
            rangeAttackAnimation();
        }
    }
    async Task getStat()
    {
        await Task.Delay(1);
        isAgro = GetComponent<StatManager>().stat.isAgro;
    }

    async void rangeAttackAnimation()
    {
        if (soundEffectDetails.kuntiAttackSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.kuntiAttackSoundEffect);
        }

        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        animator.Play("Shoot");
        
        
        
        
    }

    async void rangeAttack()
    {
        ///this.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.position = this.transform.position;
        this.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.position = this.transform.GetChild(1).gameObject.transform.position;
        this.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.GetComponent<projectileLogic>().shoot();
        
        await isCooldownRA();
    }

    void indexCount()
    {
        index += 1;
        if (index == 7){index = 0;}
    }

    void animationEventEndAttacking()
    {
        animator.Play("Idle");
    }

    async Task isCooldownRA()
    {
        rangeAttackIsCooldown = true;
        await Task.Delay(1500);
        indexCount();
        rangeAttackIsCooldown = false;
    }

    public async void attack()
    {   
        
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        animator.Play("Slash");
        await isCooldownMA();
    }

    public void triggered()
    {
        if (meleeAttakIsCooldown == false)
        {
            attack();
        }
    }

    async Task isCooldownMA()
    {
        meleeAttakIsCooldown = true;
        await Task.Delay(1000);
        meleeAttakIsCooldown = false;
    }
}
