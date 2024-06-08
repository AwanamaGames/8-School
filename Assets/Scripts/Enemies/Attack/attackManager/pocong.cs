using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocongAttackManager : MonoBehaviour, IAttack
{
    public bool agro = false;

    void update(){
        ///if agro && inScan
    }

    void attack(){
        Debug.Log("testt");
    }

    void jumpAttack()
    {
        ///stop moving
        ///switch case ke 8 arah menurut roration dari scan, play attack ke arah itu
    }
    public void triggered()
    {
        attack();
    }
}
