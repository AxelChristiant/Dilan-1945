using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private Sprite[] HeartSprites;
    [SerializeField]
    private Image HeartUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int health = EnemyScript.Instance.Health;
        if (health<=0) {
            HeartUI.sprite = HeartSprites[0];
        }
        else {

            HeartUI.sprite = HeartSprites[health];
        }
    }
}
