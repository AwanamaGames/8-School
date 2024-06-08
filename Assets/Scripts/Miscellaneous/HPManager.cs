using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class HPManager : MonoBehaviour
{

    [SerializeField] public HPSO OriginHP;
    public HPSO HP;
    
    // Start is called before the first frame update
    async void Start()
    {
        await GetHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async Task GetHP()
    {
        await Task.Delay(1);
        HP = Instantiate(OriginHP);
        HP.maxHP = GetComponent<StatManager>().stat.maxHP;
        HP.currentHP = GetComponent<StatManager>().stat.currentHP;
    }
    void takeDMG(int damage){
        HP.currentHP -= damage;
    }
}
