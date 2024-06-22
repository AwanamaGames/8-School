using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    private pStatManager player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player = coll.gameObject.GetComponent<pStatManager>();
            int damage = player.stat.currentHP / 13 + 5 - (player.stat.def / 2);
            player.GetComponent<pStatManager>().takeDMG(damage);

        }
    }
}
