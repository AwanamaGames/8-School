    using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

    public List<attackSO> attackL;
    public List<attackSO> attackLU;
    public List<attackSO> attackU;
    public List<attackSO> attackRU;
    public List<attackSO> attackR;
    public List<attackSO> attackRD;
    public List<attackSO> attackD;
    public List<attackSO> attackLD;

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
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        ExitAttack();
        //MouseAngle();
    }
    void MouseAngle(){
        Vector3 test = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            
        float i = Mathf.Atan2 (test.y, test.x) * Mathf.Rad2Deg;


        if (i >= 0 && i <=23) {attackAngle = 1;}
        else if (i >= -23 && i <= 0) {attackAngle = 1;}
        else if (i > 23 && i < 77) {attackAngle = 2;}
        else if (i >= 77 && i <= 113) {attackAngle = 3;}
        else if (i > 113 && i < 157) {attackAngle = 4;}
        else if (i >= 157 && i <= 180 || i >= -157 && i <= -180) {attackAngle = 5;}
        else if (i > -157 && i < -113) {attackAngle = 6;}
        else if (i >= -113 && i <= -77) {attackAngle = 7;}
        else if (i > -77 && i < -23) {attackAngle = 8;}
        

    }
    void Attack(){

        if (comboDelay == false && comboCounter <= attackD.Count){
            CancelInvoke("newEndCombo");

            if(attackDelay == false){

                // Play attack sound if there is one
                if (soundEffectDetails.playerAttackSoundEffect != null)
                {
                    SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.playerAttackSoundEffect);
                }

                MouseAngle();


                switch(attackAngle){
                    case 1:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackR[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", 1);
                        animator.SetFloat("Vertical", 0);
                        animator.SetFloat("attHorizontal", 1);
                        animator.SetFloat("attVertical", 0);
                    break;
                    
                    case 2:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackRU[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", 1);
                        animator.SetFloat("Vertical", 1);
                        animator.SetFloat("attHorizontal", 1);
                        animator.SetFloat("attVertical", 1);
                    break;
                    case 3:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackU[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", 0);
                        animator.SetFloat("Vertical", 1);
                        animator.SetFloat("attHorizontal", 0);
                        animator.SetFloat("attVertical", 1);
                    break;
                    case 4:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackLU[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", -1);
                        animator.SetFloat("Vertical", 1);
                        animator.SetFloat("attHorizontal", -1);
                        animator.SetFloat("attVertical", 1);
                    break;
                    case 5:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackL[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", -1);
                        animator.SetFloat("Vertical", 0);
                        animator.SetFloat("attHorizontal", -1);
                        animator.SetFloat("attVertical", 0);
                    break;
                    case 6:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackLD[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", -1);
                        animator.SetFloat("Vertical", -1);
                        animator.SetFloat("attHorizontal", -1);
                        animator.SetFloat("attVertical", -1);
                    break;
                    case 7:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackD[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", 0);
                        animator.SetFloat("Vertical", -1);
                        animator.SetFloat("attHorizontal", 0);
                        animator.SetFloat("attVertical", -1);
                    break;
                    case 8:
                        animator.Play($"Attack{comboCounter}");
                        ///animator.runtimeAnimatorController = attackRD[comboCounter].animatorOV;
                        animator.SetFloat("Horizontal", 1);
                        animator.SetFloat("Vertical", -1);
                        animator.SetFloat("attHorizontal", 1);
                        animator.SetFloat("attVertical", -1);
                    break;
                    
                }

                ///animator.Play("Attack");
                comboCounter++; 
                ///await EndAttack();
                
                ///Debug.Log("attack");

                if (comboCounter >= attackD.Count){
                    comboCounter = 0;
                    


                    ///Debug.Log("combo");
                }
            }
        }
    }

    void ExitAttack(){
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            Invoke("newEndCombo", 1);
        }
    }

    void newEndCombo()
    {
        comboCounter = 0;
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
