using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutCallback : MonoBehaviour
{

    public Image FadeImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeInOut(Action inCall, Action outCall)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeInOutCoro(inCall, outCall));
    }
    IEnumerator FadeInOutCoro(Action incallback, Action outcallback)
    {
        Color col = FadeImage.color;
        // Fade it in
        while (col.a < 1.0f)
        {
            col.a += Time.deltaTime * 1.5f;
            FadeImage.color = col;
            yield return new WaitForEndOfFrame();
        }

        incallback.Invoke();

        // Now Fade Out
        while (col.a > 0.0f)
        {
            col.a -= Time.deltaTime * 2.5f;
            FadeImage.color = col;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);

        outcallback.Invoke();



    }


}
