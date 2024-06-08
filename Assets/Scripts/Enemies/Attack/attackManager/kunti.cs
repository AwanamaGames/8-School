using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class kuntiAttackManager : MonoBehaviour, IAttack
{
    public bool isAgro;
    public NavMeshAgent agent;
    GameObject player;
    private int index = 0;
    private bool rangeAttackIsCooldown = false;
    private bool meleeAttakIsCooldown = false;
    
    async void Start()
    {
        await getStat();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GetComponent<StatManager>().stat.isAgro == true && agent.velocity.x == 0 && agent.velocity.y == 0 && rangeAttackIsCooldown == false)
        {
            rangeAttack();
        }
    }
    async Task getStat()
    {
        await Task.Delay(1);
        isAgro = GetComponent<StatManager>().stat.isAgro;
    }

    async void rangeAttack()
    {   
        this.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.position = this.transform.position;
        this.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.GetComponent<projectileLogic>().shoot();
        index += 1;
        if (index == 2){index = 0;}
        await isCooldownRA();
        
    }

    async Task isCooldownRA()
    {
        rangeAttackIsCooldown = true;
        await Task.Delay(1000);
        rangeAttackIsCooldown = false;
    }

    public async void attack()
    {   
        
        float x_distance = player.transform.position.x - transform.position.x;
        float y_distance = player.transform.position.y - transform.position.y;
        float i = Mathf.Atan2 (y_distance, x_distance) * Mathf.Rad2Deg;
        if (i >= 0 && i <=23) {Debug.Log("kanan");}
        else if (i >= -23 && i <= 0) {Debug.Log("kanan");}
        else if (i > 23 && i < 77) {Debug.Log("kanan atas");}
        else if (i >= 77 && i <= 113) {Debug.Log("atas");}
        else if (i > 113 && i < 157) {Debug.Log("kiri atas");}
        else if (i >= 157 && i <= 180 || i >= -157 && i <= -180) {Debug.Log("kiri");}
        else if (i > -157 && i < -113) {Debug.Log("kiri bawah");}
        else if (i >= -113 && i <= -77) {Debug.Log("bawah");}
        else if (i > -77 && i < -23) {Debug.Log("kanan bawah");}
        await isCooldownMA();
    }

    public void triggered()
    {
        if (meleeAttakIsCooldown == false)
        {
            attack();
        }
    }

    async Task isCooldownMA()
    {
        meleeAttakIsCooldown = true;
        await Task.Delay(1000);
        meleeAttakIsCooldown = false;
    }
}
