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
    public bool isJumping = false;
    StatManager statManager;

    public NavMeshAgent agent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        statManager = GetComponent<StatManager>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        movAni();
    }
    void FixedUpdate()
    {
        if (statManager.isAgro && isJumping == false && statManager.inRange == false)
        {
            jump();
        }
        if (isJumping == false && statManager.inRange == true)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void jump()
    {   
        agent.SetDestination(player.transform.position);
        animator.SetBool("isJumping", true);

    }

    void movAni()
    {
        if (isJumping == true)
        {
            animator.SetFloat("x", agent.velocity.x);
            animator.SetFloat("y", agent.velocity.y);
        }
    }

    void jatuh()
    {
        agent.SetDestination(transform.position);
    }


    
}
