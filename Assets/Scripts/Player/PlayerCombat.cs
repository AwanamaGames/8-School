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
    float attackAngle;
    float delayAngle;
    bool togle;

    


    [SerializeField] bool attackDelay;
    [SerializeField] bool comboDelay;
    Animator animator;


    [SerializeField] private Camera cam;
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
    }
    void MouseAngle(){
        Vector3 test = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            
        float i = Mathf.Atan2 (test.y, test.x) * Mathf.Rad2Deg;


        if (i >= 0 && i <=23) {attackAngle = 270;}
        else if (i >= -23 && i <= 0) {attackAngle = 270;}
        else if (i > 23 && i < 77) {attackAngle = 315;}
        else if (i >= 77 && i <= 113) {attackAngle = 0;}
        else if (i > 113 && i < 157) {attackAngle = 45;}
        else if (i >= 157 && i <= 180 || i >= -157 && i <= -180) {attackAngle = 90;}
        else if (i > -157 && i < -113) {attackAngle = 135;}
        else if (i >= -113 && i <= -77) {attackAngle = 180;}
        else if (i > -77 && i < -23) {attackAngle = 225;}
        

    }
    async Task Attack(){
        if (comboDelay == false && comboCounter <= attackD.Count){
            CancelInvoke("EndCombo");

            if(attackDelay == false){
                MouseAngle();


                switch(attackAngle){
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
