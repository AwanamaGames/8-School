using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhitbox : MonoBehaviour
{
    [SerializeField] StatManager thisStat;
    pStatManager player;
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
            int damage = thisStat.stat.att - player.stat.def;
            player.GetComponent<pStatManager>().takeDMG(damage);

        }
    }
}
