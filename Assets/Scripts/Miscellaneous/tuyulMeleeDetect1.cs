using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tuyulMeleeDetect : MonoBehaviour
{
    public tuyulAttackManager attackManager;
    void Start()
    {
        attackManager = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<tuyulAttackManager>();
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            attackManager.triggered();
        }
    }
    
}