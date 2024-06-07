using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pHPManager : MonoBehaviour
{

    [SerializeField] public HPSO OriginHP;
    public HPSO HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = Instantiate(OriginHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void takeDMG(int damage){
        HP.currentHP -= damage;
    }
}
