using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{

    [SerializeField]
    private int Force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerScript.Instance.Jump = false;
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.attachedRigidbody.AddForce(new Vector2(0,Force));

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
