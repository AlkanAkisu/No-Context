using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    bool isMoved = true;
    [SerializeField] double damage = 10;
    Animator animator;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator =  GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
            if(Input.GetKeyDown(KeyCode.Space))
                setSlash();
        
        
    }
    

    // Functions
    public double getDamage()
    {
        return damage;
    }

    public void moveXByForce(float force){
        Vector3 forceVector = new Vector3(force,0,0);
        rb.AddForce(forceVector);
    }
    public void faceRight()
    {
        transform.localScale = new Vector3(-1,1,1);
    }
    public void faceLeft()
    {
        transform.localScale = new Vector3(1,1,1);
    }
    

    public void moveXByTranslate(float displacement){
        //Vector3 displacementVector = new Vector3(displacement,0,0);
        //Vector3 translateVector = displacementVector + transform.position;
        transform.Translate(new Vector3(displacement,0,0));
    }

    public void setSlash(){
            animator.SetTrigger("Slash");
        
        
    }
}
