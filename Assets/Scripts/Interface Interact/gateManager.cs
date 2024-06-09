using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManager : MonoBehaviour, IInteractable
{   
    public bool locked;
    public GameObject boss;
    public void Interact()
    {
        if (locked == false)
        {   
            ///trigger save
            ///lanjut ke scene selanjutnya
        }else
        {
            Instantiate(boss, transform.position, Quaternion.identity);
            ///if boss die, 'boss stat manager' will findgameobjectwith tag Gate, access this script and turn canMove to true;
        }
    }
}
