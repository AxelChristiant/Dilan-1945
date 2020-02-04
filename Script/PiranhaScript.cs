using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaScript : MonoBehaviour
{

    private Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myRigidBody.velocity.y <= 0) {
            transform.localRotation = Quaternion.Euler(Vector3.forward * 180);
            if (transform.position.y <= -2f)
            {

                Destroy(gameObject);
            }
        }
    }





}
