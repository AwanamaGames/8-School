using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class attackManager : MonoBehaviour
{
    async void Start()
    {
        await getStat();
    }

    async Task getStat()
    {
        await Task.Delay(1);
        bool isAgro = GetComponent<StatManager>().stat.isAgro;
    }

    void rangeAttack()
    {
        
    }
}
