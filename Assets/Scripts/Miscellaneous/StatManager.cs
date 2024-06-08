using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Experimental;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] public StatSO originStat;
    public StatSO stat;


    async void Start()
    {   
        stat = Instantiate(originStat);
        
    }

    void Update()
    {
        
    }

    #region Original takeDMG method
    //public void takeDMG(int damage)
    //{   
    //    Debug.Log(damage);
    //    stat.currentHP -= damage;
    //    if (stat.currentHP <= 0)
    //    {   
    //        GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>().stat.leaf += stat.leaf;
    //        GameObject.Destroy(gameObject);
    //    }
    //}
    #endregion Original takeDMG method

    public void takeDMG(int damage) // Revised for the new item "Double Leaf"
    {
        Debug.Log(damage);
        stat.currentHP -= damage;
        if (stat.currentHP <= 0)
        {
            pStatManager playerStatManager = GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>();
            int leafToAdd = stat.leaf;

            // Check if double leaf effect is active
            if (playerStatManager.isDoubleLeafActive)
            {
                leafToAdd *= 2; // Double the leaf amount
            }

            playerStatManager.stat.leaf += leafToAdd;
            Destroy(gameObject);
        }
    }

}
