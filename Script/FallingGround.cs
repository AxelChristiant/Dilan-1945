using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{

    private Rigidbody2D rb;

    private float fallDelay;
    private Vector2 ThisPosition;

    // Start is called before the first frame update
    void Start()
    {
        fallDelay = 0.5f;
        rb = GetComponent<Rigidbody2D>();
        ThisPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) {
            FallingGroundManager.Instance.StartCoroutine("spawnPlatform", ThisPosition);

            StartCoroutine(fall());
            Destroy(gameObject,2f);
            
        }
    }

    IEnumerator fall() {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
