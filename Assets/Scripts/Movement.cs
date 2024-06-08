using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
   NavMeshAgent agent;
   StatSO stat;
   GameObject target;
   public bool statOK = false;
   Animator animator;

   public Rigidbody2D rb;

   public bool onKnockback = false;

   public Vector2 knockback;
   Vector2 incomingknock;

   async void Start()
   {
    rb = GetComponent<Rigidbody2D>();
    target = GameObject.FindGameObjectWithTag("Player");
    agent = GetComponent<NavMeshAgent>();
    
    agent.updateRotation = false;
    agent.updateUpAxis = false;

    animator = GetComponent<Animator>();


    await getStat();

    
   }

   void Update()
   {
    rotateTo();

    if (stat.isAgro == true)
    {
      agent.SetDestination(target.transform.position);
    }
    movAni();


    
   }
  void FixedUpdate()
  {
    
  }

   async Task getStat()
   {
    await Task.Delay(1);
    stat = GetComponent<StatManager>().stat;
    agent.speed = stat.movSpd;
    statOK = true;
   }



   void rotateTo()
   {
        float x_distance = target.transform.position.x - transform.position.x;
        float y_distance = target.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2 (y_distance, x_distance) * Mathf.Rad2Deg;
        ///if (angle<0) angle += 360;
        this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
   }

  void movAni()
  {
        if (agent.velocity.x == 0 && agent.velocity.y == 0){
            animator.SetBool("isMoving", false);
        } else {
            animator.SetBool("isMoving", true);
            animator.SetFloat("x", agent.velocity.x);
            animator.SetFloat("y", agent.velocity.y);
        }
  }


  
  

}
