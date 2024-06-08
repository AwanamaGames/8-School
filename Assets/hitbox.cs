using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
            
            UnityEngine.Vector2 difference = (coll.gameObject.transform.position - player.transform.position).normalized;

            UnityEngine.Vector2 force = difference * 2;

            await coll.gameObject.GetComponent<StatManager>().Onknockback(force, 1000);


           totalDamage = player.GetComponent<pStatManager>().stat.att - coll.gameObject.GetComponent<StatManager>().stat.def;
           player.GetComponent<pStatManager>().CallItemOnHit(this, coll.gameObject.GetComponent<StatManager>());
           coll.gameObject.GetComponent<StatManager>().takeDMG(totalDamage);
           
            
            ///coll.gameObject.GetComponent<Movement>().knockback += new UnityEngine.Vector2(0, 3);
            

            
            


        }
    }
}
