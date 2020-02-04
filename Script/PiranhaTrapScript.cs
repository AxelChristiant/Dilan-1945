using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaTrapScript : MonoBehaviour
{

    [SerializeField]
    private GameObject Piranha;

    private float Timer;
    private float IdleTimer;
    
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        PiranhaJump();
    }

    private void PiranhaJump() {
       
        Timer += Time.deltaTime;
        if (Timer >= 3) {
            Timer = 0;
            GameObject tmp = (GameObject)Instantiate(Piranha, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            tmp.GetComponent<Rigidbody2D>().AddForce( new Vector2(0,600));
            
           
        }


    }
}
