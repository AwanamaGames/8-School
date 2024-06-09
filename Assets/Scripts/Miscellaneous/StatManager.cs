using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Experimental;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] public StatSO originStat;
    [SerializeField] FloatingHealthBar healthBar;
    public StatSO stat;


    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    async void Start()
    {   
        stat = Instantiate(originStat);

    }

    void Update()
    {
        healthBar.UpdateHealthBar((float)stat.currentHP, (float)stat.maxHP);
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

    // New method to handle leaf addition
    private void AddLeafsToPlayer()
    {
        pStatManager playerStatManager = GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>();
        int leafToAdd = stat.leaf;

        // Check if double leaf effect is active
        if (playerStatManager.isDoubleLeafActive)
        {
            leafToAdd *= 2; // Double the leaf amount
        }

        playerStatManager.stat.leaf += leafToAdd;
    }

    public void takeDMG(int damage) // Revised for the new item "Double Leaf"
    {
        Debug.Log(damage);
        stat.currentHP -= damage;

        if (stat.currentHP <= 0)
        {
            AddLeafsToPlayer(); // Call the new method
            Destroy(gameObject);
        }
    }


}
