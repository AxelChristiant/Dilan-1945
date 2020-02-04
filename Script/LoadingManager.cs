using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private Text SwordLevel,GunLevel,PunchLevel,HealthLevel;
    [SerializeField]
    private Slider slider;

    private float Timer;
    private bool LoadingIsEnd;

    
    
    // Start is called before the first frame update
    void Start()
    {
        SwordLevel.text = GameManager.Instance.PLAYERSWORDLEVEL.ToString();
        PunchLevel.text = GameManager.Instance.PLAYERHANDLEVEL.ToString();
        GunLevel.text = GameManager.Instance.PLAYERGUNLEVEL.ToString();
        HealthLevel.text = GameManager.Instance.PLAYERHEALTHLEVEL.ToString();
        LoadingIsEnd = false;
        
    }

    // Update is called once per frame
    void Update()
    {
 
       Timer += Time.deltaTime;
        slider.value = Timer/3f;
        if (Timer >= 3f && !LoadingIsEnd) {
            LoadingIsEnd = true;
            slider.value = 1f;
            GoToNextLevel();
            Timer = 0;


        }
    }

    void GoToNextLevel() {
        GameManager.Instance.RecentLevel += 1;
        SceneManager.LoadScene(GameManager.Instance.RecentLevel);
        
    }
}
