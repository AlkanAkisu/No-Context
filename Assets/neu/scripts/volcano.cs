using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volcano : MonoBehaviour
{
    Animator anime;
    double startTime;
    [SerializeField] float eruptRandRange;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     erupt();
        // }
        //randomErupt();
       
    }


    void erupt(){
        anime.SetTrigger("erupt");
        float random = Random.Range(0f,eruptRandRange);
        Invoke(nameof(erupt),random);
        Debug.Log(random);
    }

    public void randomErupt(){
            
            Invoke(nameof(erupt),Random.Range(0f,eruptRandRange));
            
        }
    }



