using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DeadUI;

    [SerializeField]
    private GameObject QuizUI;

    [SerializeField]
    private GameObject Warning;

    [SerializeField]
    private Button AnswerA, AnswerB, AnswerC, AnswerD;
    [SerializeField]
    private Text TextQuestion, TextAnswerA, TextAnswerB, TextAnswerC, TextAnswerD;
    [SerializeField]
    private GameObject TutorialUI;
    [SerializeField]
    private Sprite[] TutorialSprite;
    [SerializeField]
    private Image TutorialImage;
    public bool StartWithTutorial;
    private int TutorialSlide;



    
    private bool AIsRightAnswer, BIsRightAnswer, CIsRightAnswer, DIsRightAnswer;
    private static UIManager instance;

    private Chest ChestInstance;

    private void OnEnable()
    {

    
    
            AnswerA.onClick.AddListener(() => AnswerIsRight(ChestInstance.AIsRightAnswer));
            AnswerB.onClick.AddListener(() => AnswerIsRight(ChestInstance.BIsRightAnswer));
            AnswerC.onClick.AddListener(() => AnswerIsRight(ChestInstance.CIsRightAnswer));
            AnswerD.onClick.AddListener(() => AnswerIsRight(ChestInstance.DIsRightAnswer));
     

    }
    void OnDisable()
    {
   
            AnswerA.onClick.RemoveAllListeners();
            AnswerB.onClick.RemoveAllListeners();
            AnswerC.onClick.RemoveAllListeners();
            AnswerD.onClick.RemoveAllListeners();
      
    }

    public static UIManager Instance {
        get {
            if(instance==null)
                instance = GameObject.FindObjectOfType<UIManager>();
            return instance;
        }
       
    }
    void Start()
    {
        DeadUI.SetActive(false);
        QuizUI.SetActive(false);
        TutorialSlide = 0;
        if (StartWithTutorial)
        {
            
            showTheTutorial();
        }


    }
    public void showTheTutorial() {
        Time.timeScale = 0;
        TutorialImage.sprite = TutorialSprite[TutorialSlide];
        TutorialUI.SetActive(true);
        TutorialSlide += 1;
    }
    public void closeTheTutorial() {
        StartWithTutorial = false;
        Time.timeScale = 1;
        TutorialUI.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (StartWithTutorial)
            Time.timeScale = 0;
    }

    public void RestartGame() {
       
        GameManager.Instance.Restart();
    }

    public void OnDeadUI() {
        Time.timeScale = 0f;
        DeadUI.SetActive(true);
    }
    public void toMainMenu() {
        GameManager.Instance.ResetEverything();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void OpenTheQuiz(Chest ChestInstance) {
        Time.timeScale = 0f;
        QuizUI.SetActive(true);
        this.ChestInstance = ChestInstance;
        TextQuestion.text = ChestInstance.Question;
        TextAnswerA.text = ChestInstance.A;
        TextAnswerB.text = ChestInstance.B;
        TextAnswerC.text = ChestInstance.C;
        TextAnswerD.text = ChestInstance.D;
        AIsRightAnswer = ChestInstance.AIsRightAnswer;




    }
    private void AnswerIsRight( bool RightAnswer) {
        if (RightAnswer)
        {
            Time.timeScale = 1;
            QuizUI.SetActive(false);
            ChestInstance.OpenChest();
            ChestInstance = null;
        }
        else {
            QuizUI.SetActive(false);
            Time.timeScale = 1;
            ChestInstance = null;
        }
    }

    public void LevelUpHand() {
        GameManager.Instance.LevelUp(1);
    }
    public void LevelUpGun()
    {
        GameManager.Instance.LevelUp(3);
    }

    public void LevelUpSword()
    {
        GameManager.Instance.LevelUp(2);
    }

    public void LevelUpHealth()
    {
        GameManager.Instance.LevelUp(4);
    }

}
