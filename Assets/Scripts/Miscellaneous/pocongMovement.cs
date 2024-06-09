using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class pocongMovement : MonoBehaviour
{
    public StatSO stat;
    public GameObject player;
    public Animator animator;
    public bool isCooldown = false;

    public NavMeshAgent agent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    async void FixedUpdate()
    {
        if (GetComponent<StatManager>().stat.isAgro && isCooldown == false){await jump();}
        await jumpTrack();

    }

    async Task jump()
    {   
        isCooldown = true;
        animator.Play("test");
        agent.SetDestination(player.transform.position);
        isCooldown =  true;

    }

    async Task jumpTrack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("test"))
        {
            agent.SetDestination(transform.position);
            isCooldown = true;
            await Task.Delay(2000);
            isCooldown = false;
        }
    }
}
