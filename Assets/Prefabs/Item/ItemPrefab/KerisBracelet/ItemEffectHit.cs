using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemEffectHit : MonoBehaviour
{
    public GameObject player;
    public int totalDamage;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
   async void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Enemy")
        {   
            ///player attack - enemy def
           totalDamage = (player.GetComponent<pStatManager>().stat.att - coll.gameObject.GetComponent<StatManager>().stat.def) * 2;

            ///damaging
           coll.gameObject.GetComponent<StatManager>().takeDMG(totalDamage);

            try
            {

            ///Knockback; 2 is force multiplier, 1000 is duration of knock
            UnityEngine.Vector2 arah = (coll.gameObject.transform.position - player.transform.position).normalized;

            UnityEngine.Vector2 force = arah * 3;

            await coll.gameObject.GetComponent<StatManager>().Onknockback(force, 500);
            }
            catch
            {
                
            }




           
        }
    } 


}
