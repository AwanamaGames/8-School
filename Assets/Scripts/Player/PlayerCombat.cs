    using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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
    public int comboCounter;

    float angle;
    float newAngle;
    float delayAngle;
    bool togle;


    [SerializeField] bool attackDelay;
    [SerializeField] bool comboDelay;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        delayAngle = -5f;
        lastComboEnd = -2f;
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            await Attack();
        }
        ExitAttack();
        //MouseAngle();
        Toggle();
    }
    void MouseAngle(){
        if(Time.time - delayAngle > 0.5f){
        angle = Mathf.Atan2 (Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * Mathf.Rad2Deg;
        if (angle<0) angle += 360;
        switch (angle){
                case float i when i >= 0 && i <=23:
                    newAngle = 0;
                    break;
                case float i when i >= 337 && i <= 369:
                    newAngle = 0;
                    break;
                case float i when i > 23 && i < 77:
                    newAngle = 45;
                    break;
                case float i when i >= 77 && i <= 113:
                    newAngle = 90;
                    break;
                case float i when i > 113 && i < 157:
                    newAngle = 135;
                    break;
                case float i when i >= 157 && i <= 203:
                    newAngle = 180;
                    break;
                case float i when i > 203 && i < 247:
                    newAngle = 225;
                    break;
                case float i when i >= 247 && i <= 303:
                    newAngle = 270;
                    break;
                case float i when i > 303 && i < 337:
                    newAngle = 315;
                    break;
            };
        
        Debug.Log(newAngle);
        delayAngle = Time.time;
        }

    }
    void Toggle(){
        if (Input.GetKeyDown(KeyCode.Space)){
            newAngle = 0;
        };
        if (Input.GetKeyDown(KeyCode.CapsLock)){
            newAngle = 180;
        }
    }
    async Task Attack(){
        if (comboDelay == false && comboCounter <= attackD.Count){
            CancelInvoke("EndCombo");

            if(attackDelay == false){
                switch(newAngle){
                    case 0:
                        animator.runtimeAnimatorController = attackU[comboCounter].animatorOV;
                    break;
                    
                    case 180:
                        animator.runtimeAnimatorController = attackD[comboCounter].animatorOV;
                    break;
                    
                }

                animator.Play("Attack");
                await EndAttack();
                
                ///Debug.Log("attack");

                if (comboCounter >= attackD.Count){
                    
                    comboDelay = true;
                    await Task.Delay(3000);
                    comboCounter = 0;
                    comboDelay = false;


                    ///Debug.Log("combo");
                }
            }
        }
    }

    void ExitAttack(){
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            Invoke("EndCombo", 1);
        }
    }

    async Task EndCombo()
    {
        comboDelay = true;
        await Task.Delay(3000);
        comboCounter = 0;
        comboDelay = false;
    }

    async Task EndAttack()
    {
        attackDelay = true;
        comboCounter++;
        await Task.Delay(500);
        attackDelay = false;
    }
}
