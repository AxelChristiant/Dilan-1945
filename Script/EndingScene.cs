using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer text1;
    [SerializeField]
    private SpriteRenderer text2;
    private int n;

    private int scene;
    void Start()
    {
        Color color1 = text1.color;
        color1.a = 0;
        text1.material.color = color1;
        Color color2 = text2.color;
        color2.a = 0;
        text2.material.color = color2;
        StartCoroutine(FadeIn(text1));


    }

    // Update is called once per frame
    void Update()
    {
        if (n >= 2) {
            GameManager.Instance.ResetEverything();
            SceneManager.LoadScene(0);
        }

        
    }




    IEnumerator FadeIn(SpriteRenderer text) {
        for (float f = 0.05f; f <= 1; f += 0.01f) {
            Color tes = text.color;
            tes.a = f;
            text.material.color = tes;
            yield return new WaitForSeconds(0.05f);
            
        }

        StartCoroutine(FadeOut(text));


    }
    IEnumerator FadeOut(SpriteRenderer text)
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color tes = text.color;
            tes.a = f;
            text.material.color = tes;
            yield return new WaitForSeconds(0.1f);

        }
        n += 1;
        StartCoroutine(FadeIn(text2));

    }
}
