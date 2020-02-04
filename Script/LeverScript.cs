using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    [SerializeField]
    private GameObject TrapGround;
    [SerializeField]
    private Sprite[] LeverSprite;
    private bool isPulled;
    private SpriteRenderer MySprite;
    
    // Start is called before the first frame update
    void Start()
    {
        isPulled = false;
        MySprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Melee") || collision.CompareTag("Sword") || collision.CompareTag("Bullets")) {
            isPulled = !isPulled;
            MySprite.sprite = LeverSprite[isPulled ? 1 : 0];
            TrapGround.GetComponent<TrapScript>().TriggeredTrap(!isPulled);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
