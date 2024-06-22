using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class bossAttackManager : MonoBehaviour
{
    public bool isAgro;
    public NavMeshAgent agent;
    GameObject player;
    private int index = 0;
    private bool rangeAttackIsCooldown = false;
    private bool meleeAttakIsCooldown = false;
    StatManager statManager;

    Animator animator;
    public bool isAttacking = false;

    public BossMovement bossMovement;

    public BossRoomManager bossRoom;
    float time;
    public List<GameObject> projectile;
    
    public Transform bulletfrom;

    private UnityEngine.Vector3 atas = new UnityEngine.Vector3(0, 20, 0);
    private UnityEngine.Vector3 kanan = new UnityEngine.Vector3(20, 0, 0);
    private UnityEngine.Vector3 bawah = new UnityEngine.Vector3(0, -20, 0);
    private UnityEngine.Vector3 kiri = new UnityEngine.Vector3(-20, 0, 0);

    private UnityEngine.Vector3 atasC = new UnityEngine.Vector3(0, 2, 0);
    private UnityEngine.Vector3 kananC = new UnityEngine.Vector3(2, 0, 0);
    private UnityEngine.Vector3 bawahC = new UnityEngine.Vector3(0, -2, 0);
    private UnityEngine.Vector3 kiriC = new UnityEngine.Vector3(-2, 0, 0);




    
    async void Start()
    {   
        statManager = GetComponent<StatManager>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        await getStat();
        animator = GetComponent<Animator>();
        bossMovement = GetComponent<BossMovement>();
        bossRoom = GameObject.FindGameObjectWithTag("BossRoom").GetComponent<BossRoomManager>();
        
    }

    void Update()
    {   
        time = Time.time;
        if (isAttacking == false && time > 1)
        {
            if (meleeAttakIsCooldown == false)
        {;
            ///attack();
            meleeAttakIsCooldown = true;
        }

            else if (rangeAttackIsCooldown == false)
        {
            Debug.Log(bulletfrom.position - bawah);
            rangeAttack();
        }
        }
    }
    async Task getStat()
    {
        await Task.Delay(1);
        isAgro = GetComponent<StatManager>().stat.isAgro;
    }

    async void rangeAttackAnimation()
    {   

        UnityEngine.Vector2 direction = (player.transform.position - transform.position).normalized;
        
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        animator.Play("Shoot");
        
        
        
        
    }

    async void rangeAttack()
    {   
        isAttacking = true;
        transform.position = bossRoom.center.position;
        for (int i = 0; i < 5; i++){
            projectile[i*4].transform.position = bulletfrom.position;
            projectile[i*4].SetActive(true);

            projectile[i*4+1].transform.position = bulletfrom.position;
            projectile[i*4+1].SetActive(true);

            projectile[i*4+2].transform.position = bulletfrom.position;
            projectile[i*4+2].SetActive(true);

            projectile[i*4+3].transform.position = bulletfrom.position;
            projectile[i*4+3].SetActive(true);


            await Task.Delay(1500);
        }
        
        await isCooldownRA();
    }



    void animationEventEndAttacking()
    {
        animator.Play("Idle");
    }

    async Task isCooldownRA()
    {
        isAttacking = false;
        rangeAttackIsCooldown = true;
        await Task.Delay(18000);
        rangeAttackIsCooldown = false;
    }

    public async void attack()
    {   
        isAttacking = true;
        agent.speed *= 5;
        bossMovement.MoveRightLeft1();
        await Task.Delay(8000);
        bossMovement.MoveLeftRight2();
        await Task.Delay(8000);
        bossMovement.MoveRightLeft3();
        await Task.Delay(8000);
        transform.position = bossRoom.center.position;
        agent.speed /= 5;
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
        isAttacking = false;
        meleeAttakIsCooldown = true;
        await Task.Delay(35000);
        meleeAttakIsCooldown = false;
    }

    public UnityEngine.Vector3 evaluateAtas(float t)
    {
        UnityEngine.Vector3 ac = UnityEngine.Vector3.Lerp(bulletfrom.position, bulletfrom.position + new UnityEngine.Vector3(-5, 0), t);
        UnityEngine.Vector3 cb = UnityEngine.Vector3.Lerp(bulletfrom.position + new UnityEngine.Vector3(-5, 0), bulletfrom.position + new UnityEngine.Vector3(0, 20), t);
        return UnityEngine.Vector3.Lerp(ac, cb, t);
    }

    public UnityEngine.Vector3 evaluateKanan(float t)
    {
        UnityEngine.Vector3 ac = UnityEngine.Vector3.Lerp(bulletfrom.position, bulletfrom.position + new UnityEngine.Vector3(0, 5), t);
        UnityEngine.Vector3 cb = UnityEngine.Vector3.Lerp(bulletfrom.position + new UnityEngine.Vector3(0, 5), bulletfrom.position + new UnityEngine.Vector3(20, 0), t);
        return UnityEngine.Vector3.Lerp(ac, cb, t);
    }

    public UnityEngine.Vector3 evaluateBawah(float t)
    {
        UnityEngine.Vector3 ac = UnityEngine.Vector3.Lerp(bulletfrom.position, bulletfrom.position + new UnityEngine.Vector3(5, 0), t);
        UnityEngine.Vector3 cb = UnityEngine.Vector3.Lerp(bulletfrom.position + new UnityEngine.Vector3(5, 0), bulletfrom.position + new UnityEngine.Vector3(0, -20), t);
        return UnityEngine.Vector3.Lerp(ac, cb, t);
    }

    public UnityEngine.Vector3 evaluateKiri(float t)
    {
        UnityEngine.Vector3 ac = UnityEngine.Vector3.Lerp(bulletfrom.position, bulletfrom.position + new UnityEngine.Vector3(0, -5), t);
        UnityEngine.Vector3 cb = UnityEngine.Vector3.Lerp(bulletfrom.position + new UnityEngine.Vector3(0, -5), bulletfrom.position + new UnityEngine.Vector3(-20, 0), t);
        return UnityEngine.Vector3.Lerp(ac, cb, t);
    }
}