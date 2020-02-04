using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int TotalEnemy;
    private static LevelManager instance;
    private bool levelCompleted;


    public static LevelManager Instance {

        get {
            if (instance == null)
                instance = GameObject.FindObjectOfType<LevelManager>();
            return instance;
        }
    }

    public bool LevelCompleted {

        get {
            return levelCompleted;
        }
    }
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update()
    {
        
        if (TotalEnemy <= 0) {
            levelCompleted = true;
            PortalScript.Instance.OpenTheGate();
        }
    }

    public void DecrementEnemy() {
        TotalEnemy -= 1;
    }
}
