using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Experimental;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] public StatSO originStat;
    public StatSO stat;
    public Rigidbody2D rb;


    async void Start()
    {   
        stat = Instantiate(originStat);
        rb = GetComponent<Rigidbody2D>();
        
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

    public async Task Onknockback(Vector2 force, int dur)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        await Task.Delay(dur);
        rb.velocity = Vector2.zero;
    }

}
