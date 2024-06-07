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

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Enemy")
        {   
           totalDamage = player.GetComponent<pStatManager>().stat.att - coll.gameObject.GetComponent<StatManager>().stat.def;
           player.GetComponent<pStatManager>().CallItemOnHit(this, coll.gameObject.GetComponent<StatManager>());
           coll.gameObject.GetComponent<StatManager>().takeDMG(totalDamage);
           ///(int)(player.GetComponent<StatManager>().stat.att)
        }
    }
}
