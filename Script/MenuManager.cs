using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ToGame() {
        Time.timeScale = 1;

        SceneManager.LoadScene(9);
    }
    public void ExitGame() {
        Application.Quit();
    }
}
