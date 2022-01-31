using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            rb.AddForce(new Vector2(-3f,0));
        }
        else if(Input.GetKey(KeyCode.D)){
            rb.AddForce(new Vector2(3f,0));
        }
        else if(Input.GetKey(KeyCode.W)){
            rb.AddForce(new Vector2(0,20f));
        }
        else{
            rb.AddForce(new Vector2(0,0));
        }
    }
}
