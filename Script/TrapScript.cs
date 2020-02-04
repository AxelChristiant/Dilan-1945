using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] trap;

    [SerializeField]
    private bool isTrigger;

    private float Duration = 1;
    private float Timer;
    void OnTriggerEnter2D(Collider2D collision)
    {



        if ((collision.tag == "Player" || collision.tag == "Crate")&&!isTrigger)
            {
            StartCoroutine(enableTrap());   
               
        } 
    }
    void OnTriggerExit2D(Collider2D collision)
    {

        
        if(!isTrigger)
            StartCoroutine(disableTrap());
        
    }
     public IEnumerator disableTrap() {
        yield return new WaitForSeconds(2f);


            for (int i = 0; i < trap.Length; i++)
            {

                trap[i].SetActive(false);
            }
           

    }
    public IEnumerator enableTrap() {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < trap.Length; i++)
        {

            trap[i].SetActive(true);
        }
        
    }

    public void TriggeredTrap( bool isPulled)
    {
        for (int i = 0; i < trap.Length; i++)
        {

            trap[i].SetActive(isPulled);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
