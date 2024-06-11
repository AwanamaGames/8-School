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
        
        ///movAni();
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
        attackTriggerRotateTo();
    }

    void jump()
    {   
        agent.SetDestination(player.transform.position);
        animator.SetBool("isJumping", true);
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);

    }

    void movAni()
    {
        if (isJumping == true)
        {
            ///animator.SetFloat("x", agent.velocity.x);
            ///animator.SetFloat("y", agent.velocity.y);
        }
    }

    void jatuh()
    {
        agent.SetDestination(transform.position);
    }

    void attackTriggerRotateTo()
    {
        float x_distance = player.transform.position.x - transform.position.x;
        float y_distance = player.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2(y_distance, x_distance) * Mathf.Rad2Deg;
        ///if (angle<0) angle += 360;
        this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }



    
}
