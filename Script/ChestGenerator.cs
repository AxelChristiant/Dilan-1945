using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject chest;

    private float Timer;
    private Transform MyPosition;
    // Start is called before the first frame update
    void Start()
    {
        MyPosition = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= 15f) {
           GameObject tmp= (GameObject)Instantiate(chest, MyPosition.position, Quaternion.identity);
            Timer = 0;
            StartCoroutine(destroyChest(tmp));
        }

        
    }

    IEnumerator destroyChest(GameObject chest) {
        yield return new WaitForSeconds(10f);
        Destroy(chest);

    }
        
        
}
