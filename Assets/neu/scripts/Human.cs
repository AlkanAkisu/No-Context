using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    Animator anime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            anime.SetBool("isWalking",true);
            walk(-5f);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            walk(5f);
            anime.SetBool("isWalking",true);
            ;
        }
        else
        {
            anime.SetBool("isWalking",false);
        }
    }

    public void walk(float velocity)
    {
        if(velocity < 0){
        rb.velocity = new Vector2(velocity,0);    
        transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            rb.velocity = new Vector2(velocity,0);
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}
