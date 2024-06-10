using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    public GameObject player;
    public int totalDamage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    async void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Enemy")
        {   
            ///player attack - enemy def
           totalDamage = player.GetComponent<pStatManager>().stat.att - coll.gameObject.GetComponent<StatManager>().stat.def;

            ///onHit item
           player.GetComponent<pStatManager>().CallItemOnHit(this, coll.gameObject.GetComponent<StatManager>());

            ///damaging
           coll.gameObject.GetComponent<StatManager>().takeDMG(totalDamage);

            try
            {

            ///Knockback; 2 is force multiplier, 1000 is duration of knock
            UnityEngine.Vector2 arah = (coll.gameObject.transform.position - player.transform.position).normalized;

            UnityEngine.Vector2 force = arah * 2;

            await coll.gameObject.GetComponent<StatManager>().Onknockback(force, 1000);
            }
            catch
            {
                
            }




           
        }
    }
}
