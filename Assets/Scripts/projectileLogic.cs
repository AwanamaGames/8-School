using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class projectileLogic : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> listSprite;
    public globalTime globaltime;
    public int offset;

    GameObject player;
    Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {   
        globaltime = GameObject.FindGameObjectWithTag("globaltime").GetComponent<globalTime>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        offset = Random.Range(0, 2);
        
        
        
    }

    public void shoot()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        UnityEngine.Vector2 direction = player.transform.position - transform.position;
        rb.velocity =  new UnityEngine.Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        changeSprite();
    }

    void changeSprite()
    {
        float shootTime = globaltime.global;
        int frame = (int)((shootTime * 1.5f + offset) % listSprite.Count);

        spriteRenderer.sprite = listSprite[frame];
    }
}
