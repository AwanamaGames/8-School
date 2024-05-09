using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public List<attackSO> attackL;
    public List<attackSO> attackLU;
    public List<attackSO> attackU;
    public List<attackSO> attackUR;
    public List<attackSO> attackR;
    public List<attackSO> attackLRD;
    public List<attackSO> attackD;
    public List<attackSO> attackDL;

    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    float angle;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Attack();
        }
        ExitAttack();

    }
    void MouseAngle(){
        angle = Mathf.Atan2 (Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * Mathf.Rad2Deg;
        if (angle<0) angle += 360;
            float newAngle = angle switch{
                float i when i >= 0 && i <=23 => 0,
                float i when i >= 337 && i <= 369 => 0,
                float i when i > 23 && i < 77 => 45,
                float i when i >= 77 && i <= 113 => 90,
                float i when i > 113 && i < 157 => 135,
                float i when i >= 157 && i <= 203 => 180,
                float i when i > 203 && i < 247 => 225,
                float i when i >= 247 && i <= 303 => 270,
                float i when i > 303 && i < 337 => 315,
            };

    }
    void Attack(){
        if (Time.time - lastComboEnd > 0.5f && comboCounter <= attackD.Count){
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= 0.2f){
                animator.runtimeAnimatorController = attackD[comboCounter].animatorOV;
                animator.Play("Attack");
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= attackD.Count){
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack(){
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo(){
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
