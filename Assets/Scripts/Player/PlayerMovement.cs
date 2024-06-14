using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        MoveAnimation();
        Movement();


    }

    void Movement()
    {
        movHorizontal = Input.GetAxis("Horizontal");
        movVertical = Input.GetAxis("Vertical");
        direction = new Vector2(movHorizontal, movVertical);
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            body.velocity = direction.normalized * GetComponent<pStatManager>().stat.movSpd * 0.4f;
        }else 
        {
        body.velocity = direction.normalized * GetComponent<pStatManager>().stat.movSpd;
        }
    }

    void MoveAnimation()
    {
        if (movHorizontal == 0 && movVertical == 0){
            animator.SetBool("isMoving", false);
        } else {
            animator.SetBool("isMoving", true);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
    }
}
