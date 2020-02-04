using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator myAnimator;

    private Collider2D Player;

    [SerializeField]
    private GameObject[] item;

    private Chest instance;

    private bool IsAnswered;
    [SerializeField]
    public string Question, A, B, C, D;
    [SerializeField]
    public bool AIsRightAnswer, BIsRightAnswer, CIsRightAnswer, DIsRightAnswer;

    private bool picked;
    
    public Chest Instance {
        get {
            if (instance == null) {
                instance = GetComponent<Chest>();
            }

            return instance;
        }
    }
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        picked = false;
        
    }


    public void BringTheQuiz() {
        if (!picked)
        {
            UIManager.Instance.OpenTheQuiz(Instance);
        }
        }
    public void OpenChest() {
        if (!picked)
        {
            int RandNumb = Random.Range(0, 3);
            myAnimator.SetBool("IsOpened", true);
            GameObject Droppeditem = (GameObject)Instantiate(item[RandNumb], new Vector3(transform.position.x, transform.position.y + 1f), Quaternion.identity);
            Physics2D.IgnoreCollision(Droppeditem.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            picked = true;
            

        }
        }



}
