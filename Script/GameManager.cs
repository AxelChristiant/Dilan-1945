using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int PlayerSkillPoint, PlayerGunLvl=1, PlayerHandLvl=1, PlayerSwordLvl=1, PlayerHealthLvl=1;
    public static GameManager instance;
    
    private int StartSkillPoint,startPlayerGunLvl=0,startPlayerHandLvl=0,startPlayerSwordLvl=0,startPlayerHealthLvl=0;
    private int cost = 4;
    private int recentLevel=0;


    public int RecentLevel {
        get {
            return recentLevel;
        }
        set {
            this.recentLevel = value;
        }
    }
    

    public static GameManager Instance {
        get {
            
                if (instance == null)
                {
                instance = GameObject.FindObjectOfType<GameManager>();
                }
                return instance;
            

        }
    }



    public int PLAYERSKILLPOINT {
        get {
            return PlayerSkillPoint;
        }
        set {
            this.PlayerSkillPoint = value;
        }
    }
    public int PLAYERGUNLEVEL {
        get {
            return PlayerGunLvl;
        }
    }
    public int PLAYERHANDLEVEL
    {
        get
        {
            return PlayerHandLvl;
        }
    }
     public int PLAYERSWORDLEVEL {
        get {
            return PlayerSwordLvl;
        }
    }

    public int PLAYERHEALTHLEVEL
    {
        get
        {
            return PlayerHealthLvl;
        }
    }


   public void Initialize()
    {
        StartSkillPoint = PlayerSkillPoint;
        startPlayerGunLvl = PlayerGunLvl;
        startPlayerHandLvl = PlayerHandLvl;
        startPlayerSwordLvl = PlayerSwordLvl;
        startPlayerHealthLvl = PlayerHealthLvl;
    }

    private void Awake()
    {

  
            if (instance == null)
            {
                instance = this;
                

            DontDestroyOnLoad(gameObject);
            


        }
            else
            {
            
            Destroy(gameObject);
            }
        

    }
    public void LevelUp(int indicator)
    {
        if (indicator == 1 && cost * (PlayerHandLvl+1) <= PlayerSkillPoint && PlayerHandLvl < 3)
        {
            PlayerSkillPoint -= (cost * PlayerHandLvl);
            PlayerHandLvl += 1;
           
        }
        else if (indicator == 2 && cost * (PlayerSwordLvl+1) <= PlayerSkillPoint && PlayerSwordLvl < 3)
        {
            PlayerSkillPoint -= (cost * PlayerSwordLvl);
            PlayerSwordLvl += 1;
           

        }
        else if (indicator == 3 && cost * (PlayerGunLvl+1) <= PlayerSkillPoint && PlayerGunLvl < 3)
        {
            PlayerSkillPoint -= (cost * PlayerGunLvl);
            PlayerGunLvl += 1;
        }
        else if (indicator == 4 && cost * (PlayerHealthLvl+1) <= PlayerSkillPoint && PlayerHealthLvl < 3)
        {
            PlayerSkillPoint -= (cost * PlayerHealthLvl);
            PlayerHealthLvl += 1;
           
        }

    }

    public void Restart() {

        string scene = SceneManager.GetActiveScene().name;

        Reinitialize();
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        
       
        
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
        

    }
    void Reinitialize() {
        instance.PlayerSkillPoint= StartSkillPoint;
        instance.PlayerGunLvl = startPlayerGunLvl;
        instance.PlayerHandLvl= startPlayerHandLvl;
        instance.PlayerSwordLvl= startPlayerSwordLvl;
        instance.PlayerHealthLvl= startPlayerHealthLvl;
    }
    public void ResetEverything() {
        PlayerSkillPoint = 0;
        PlayerGunLvl = 1;
        PlayerHandLvl = 1;
        PlayerSwordLvl = 1;
        PlayerHealthLvl = 1;
        recentLevel = 0;
    }
}
