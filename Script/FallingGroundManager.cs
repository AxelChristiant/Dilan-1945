using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGroundManager : MonoBehaviour
{
    public static FallingGroundManager Instance = null;

    [SerializeField]
   private GameObject fallingGround;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator spawnPlatform(Vector2 spawnPosition) {
        yield return new WaitForSeconds(2f);
        Instantiate(fallingGround, spawnPosition, fallingGround.transform.rotation);
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
