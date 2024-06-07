using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class kuntiMeleeDetect : MonoBehaviour
{
    public IAttack attackManager;
    void Start()
    {
        attackManager = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<IAttack>();
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            attackManager.triggered();
        }
    }
    
}