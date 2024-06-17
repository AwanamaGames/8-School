using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class KananBossProjectileLogic : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> listSprite;
    public globalTime globaltime;
    public int offset;

    GameObject player;
    Rigidbody2D rb;
    public float force;

    private float sampleTime;
    private float speed;

    public bossAttackManager bossAttackManager;

    private Transform origin;


    // Start is called before the first frame update
    void Start()
    {   
        globaltime = GameObject.FindGameObjectWithTag("globaltime").GetComponent<globalTime>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        offset = Random.Range(0, 2);

        sampleTime = 0f;
        speed = 0.1f;
        
        
        
    }


    // Update is called once per frame
    async void Update()
    {   
        await Task.Delay(1);
        changeSprite();
        sampleTime += Time.deltaTime * speed;
        transform.position = bossAttackManager.evaluateKanan(sampleTime);

        if (sampleTime >= 1f)
        {
            sampleTime = 0;
            this.gameObject.SetActive(false);
            
        }
    }

    void changeSprite()
    {
        float shootTime = globaltime.global;
        int frame = (int)((shootTime * 1.5f + offset) % listSprite.Count);

        spriteRenderer.sprite = listSprite[frame];
    }
}
