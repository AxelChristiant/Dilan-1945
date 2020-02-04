using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Sprite[] HeartSpritesLVL1;
    [SerializeField]
    private Sprite[] HeartSpritesLVL2;
    [SerializeField]
    private Sprite[] HeartSpritesLVL3;
    [SerializeField]
    private Image HeartUI;
    [SerializeField]
    private Text SpUI;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        SpUI.text = GameManager.Instance.PLAYERSKILLPOINT.ToString();
      


    }
    private void CheckHealth()
    {

        int LevelHealth = GameManager.Instance.PLAYERHEALTHLEVEL;
        if (LevelHealth == 1)
        {
            HeartUI.sprite = PlayerScript.Instance.Health>0?HeartSpritesLVL1[PlayerScript.Instance.Health]: HeartSpritesLVL1[0];
        }
        else if (LevelHealth == 2)
        {
            HeartUI.sprite = PlayerScript.Instance.Health > 0 ? HeartSpritesLVL2[PlayerScript.Instance.Health] : HeartSpritesLVL2[0];
        }
        else
        {
            HeartUI.sprite = PlayerScript.Instance.Health > 0 ? HeartSpritesLVL3[PlayerScript.Instance.Health] : HeartSpritesLVL3[0];
        }
    }
}
