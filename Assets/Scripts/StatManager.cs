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

    public void takeDMG(int damage)
    {   
        Debug.Log(damage);
        stat.currentHP -= damage;
        if (stat.currentHP <= 0)
        {   
            GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>().stat.leaf += stat.leaf;
            GameObject.Destroy(gameObject);
        }
    }

}
