using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseUI;

    [SerializeField]
    private GameObject SkillUI;



    [SerializeField]
    public Text[] LevelText;

    [SerializeField]
    private Text PriceSword, PriceHand, PriceHealth, PriceGun;

    

    private bool paused = false;
    private bool abilityOpened = false;
    void Start()
    {
        PauseUI.SetActive(false);
        SkillUI.SetActive(false);
    }
    private void Update()
    {
        LevelText[0].text = GameManager.Instance.PLAYERSWORDLEVEL.ToString();
        LevelText[1].text = GameManager.Instance.PLAYERHANDLEVEL.ToString();
        LevelText[2].text = GameManager.Instance.PLAYERGUNLEVEL.ToString();
        LevelText[3].text = GameManager.Instance.PLAYERHEALTHLEVEL.ToString();
        PriceSword.text = GameManager.Instance.PLAYERSWORDLEVEL < 3 ? ((GameManager.Instance.PLAYERSWORDLEVEL+1) * 4).ToString() : "0";
        PriceHand.text = GameManager.Instance.PLAYERHANDLEVEL < 3 ? ((GameManager.Instance.PLAYERHANDLEVEL+1) * 4).ToString() : "0";
        PriceHealth.text = GameManager.Instance.PLAYERHEALTHLEVEL < 3 ? ((GameManager.Instance.PLAYERHEALTHLEVEL+1) * 4).ToString() : "0";
        PriceGun.text= GameManager.Instance.PLAYERGUNLEVEL < 3 ? ((GameManager.Instance.PLAYERGUNLEVEL+1) * 4).ToString() : "0";
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            Pause(paused);
        }
       

    }  

    public void resume() {
        paused = false;
        Pause(paused);
    }
    public void Pause(bool paused)
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        PauseUI.SetActive(paused);


    }
    public void AbilityButton() {
        abilityOpened = !abilityOpened;
        SkillUI.SetActive(abilityOpened);
        PauseUI.SetActive(!abilityOpened);
    }

}
