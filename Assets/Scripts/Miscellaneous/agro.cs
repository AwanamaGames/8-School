using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class agroManager : MonoBehaviour
{
    public StatManager stat;
    /* async void Start()
    {
        await getStat();
    } */
    
    /* async Task getStat()
    {
        await Task.Delay(1);
        stat = statManager.stat;
    } */

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Player")
        {
           stat.turnAgro();
        }
    }

    
}
