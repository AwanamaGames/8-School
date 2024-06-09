using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class permanentUpgrade : MonoBehaviour, IInteractable
{   
    private int attPrice;
    private int maxHPPrice;
    private int defPrice;

    private pStatManager player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>();
    }
    public void Interact()
    {
        ///show UI upgrade: attack, maxHP, def
        ///if (click attack)
        ///{(if player.stat.leaf <= attPrice)
        ///{leaf -= attPrice; player.originalstat.att += upgrade}}
    }
}
