using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    private static PortalScript instance;
    [SerializeField]
    private string NextLevel;
    private SpriteRenderer MySprite;
    [SerializeField]
    private Sprite OpenedGate;
    
    public static PortalScript Instance {
        get {
            if (instance == null)
                instance= GameObject.FindObjectOfType<PortalScript>();
            return instance;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelManager.Instance.LevelCompleted)
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadScene(9);
            }
        }
    }
    public void OpenTheGate() {
        MySprite.sprite = OpenedGate;
    }

    // Start is called before the first frame update
    void Start()
    {
        MySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
