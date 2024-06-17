using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    NavMeshAgent agent;
    StatSO stat;
    GameObject target;
    Animator animator;

    StatManager statManager;
    BossRoomManager bossRoom;
    
    public bool isAttacking;

    async void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        statManager = GetComponent<StatManager>();

        bossRoom = GameObject.FindGameObjectWithTag("BossRoom").GetComponent<BossRoomManager>(); 

        await getStat();


    }

    void Update()
    {
        movAni();
    }

    void Stop()
    {
        agent.SetDestination(transform.position);
    }

    async Task getStat()
    {
        await Task.Delay(1);
        stat = GetComponent<StatManager>().stat;
        agent.speed = stat.movSpd;
    }

    void rotateTo()
    {
        float x_distance = target.transform.position.x - transform.position.x;
        float y_distance = target.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2(y_distance, x_distance) * Mathf.Rad2Deg;
        ///if (angle<0) angle += 360;
        this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    void movAni()
    {
        if (agent.velocity.x == 0 && agent.velocity.y == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("x", agent.velocity.x);
            animator.SetFloat("y", agent.velocity.y);
        }
    }

    public void MoveTo(Transform To)
    {
        agent.SetDestination(To.position);
    }

    public void MoveToCenter()
    {
        MoveTo(bossRoom.center);
    }

    public void MoveToTop()
    {
        MoveTo(bossRoom.top);
    }
    public void MoveFromTo(Transform From, Transform To)
    {
        this.transform.position = From.position;
        agent.SetDestination(To.position);
    }

    public void StopMoving()
    {
        agent.SetDestination(this.transform.position);
    }

    public void MoveRightLeft1()
    {
        MoveFromTo(bossRoom.right1, bossRoom.left1);
    }
    public void MoveLeftRight2()
    {
        MoveFromTo(bossRoom.left2, bossRoom.right2);
    }
    public void MoveRightLeft3()
    {
        MoveFromTo(bossRoom.right3, bossRoom.left3);
    }

    
}
