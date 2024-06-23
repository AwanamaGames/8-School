using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class tuyulAttackManager : MonoBehaviour, IAttack
{
    public bool isAgro = false;
    private bool meleeAttakIsCooldown = false;
    private GameObject player;
    public NavMeshAgent agent;

    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

    async void Start()
    {
        await getStat();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (GetComponent<StatManager>().stat.isAgro)
        {

        }
    }



    async Task getStat()
    {
        await Task.Delay(1);
        isAgro = GetComponent<StatManager>().stat.isAgro;
    }

    public void triggered()
    {
        if (meleeAttakIsCooldown == false)
        {
            attack();
        }
    }

    public async void attack()
    {
        if (soundEffectDetails.tuyulAttackSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.tuyulAttackSoundEffect);
        }

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

    async Task isCooldownMA()
    {
        meleeAttakIsCooldown = true;
        await Task.Delay(1000);
        meleeAttakIsCooldown = false;
    }
}
