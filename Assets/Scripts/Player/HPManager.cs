using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{

    [SerializeField] public HPSO OriginHP;
    public HPSO HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = Instantiate(OriginHP);
        HP.maxHP = GetComponent<StatManager>().stat.maxHP;
        HP.currentHP = GetComponent<StatManager>().stat.currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void takeDMG(int damage){
        HP.currentHP -= damage;
    }
}
