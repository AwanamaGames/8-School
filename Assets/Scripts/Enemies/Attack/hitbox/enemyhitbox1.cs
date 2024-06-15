using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class tuyulenemyhitbox : MonoBehaviour
{
    [SerializeField] StatManager thisStat;
    [SerializeField] NavMeshAgent agent;
    pStatManager player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    async void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player = coll.gameObject.GetComponent<pStatManager>();
            int damage = thisStat.stat.att - player.stat.def;
            player.GetComponent<pStatManager>().takeDMG(damage);

            await Cooldown();

        }
    }

    async Task Cooldown()
    {
        float Temp = agent.speed;
        agent.speed = 0.1f;
        await Task.Delay(4000);
        agent.speed = Temp;

    }
}
