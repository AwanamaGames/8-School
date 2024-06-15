using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


///all enemy MeleeDetect
public class kuntiMeleeDetect : MonoBehaviour
{
    public IAttack attackManager;
    private StatManager statManager;
    void Start()
    {
        attackManager = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<IAttack>();
        statManager = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<StatManager>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PlayerBase")
        {
            statManager.inRange = true;
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PlayerBase")
        {
            attackManager.triggered();
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PlayerBase")
        {
            statManager.inRange = false;
        }
    }
    
}