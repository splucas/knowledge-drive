using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersManager : MonoBehaviour
{

    public AudioSource CorrectSound;
    public AudioSource WrongSound;

    public GameObject WrongVisual;
    public GameObject CorrectVisual;

    public Transform[] WrongIndicators;
    public Transform[] CorrectIndicators;

    List<IResetable> resetables = new List<IResetable>();

    int wrongCnt = 0;
    int correctCnt = 0;

    public int WrongCount { get { return wrongCnt; } }
    public int CorrectCount { get { return correctCnt; } }

    public bool AllDone { get { return (wrongCnt >= WrongIndicators.Length) || (correctCnt >= CorrectIndicators.Length); } }
                              
            


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        wrongCnt = 0;
        correctCnt = 0;
        foreach(IResetable i in resetables)
        {
            i.Reset();
        }
        resetables.Clear();

        foreach(Transform t in WrongIndicators)
        {
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            Color32 c = sr.color;
            c.a = 10;
            sr.color = c;
        }
    }

    public void HandleCorrectAnswer(Vector3 correctAnswerPos)
    {
        if (correctCnt < CorrectIndicators.Length) correctCnt += 1;

        CorrectSound.Play();
        GameObject go = Instantiate(CorrectVisual);
        
        AnswerCorrectController acc = go.GetComponent<AnswerCorrectController>();
        resetables.Add(acc);
        acc.ShowCorrect(CorrectIndicators[correctCnt-1], correctAnswerPos);

        

    }


    public void HandleWrongAnswer()
    {
        WrongSound.Play();
        if (wrongCnt < WrongIndicators.Length) wrongCnt += 1;
        GameObject go = Instantiate(WrongVisual);
        
        Transform targ = WrongIndicators[wrongCnt-1];

        StartCoroutine(AnimateWrong(go, targ));

        
    }

    IEnumerator AnimateWrong(GameObject go, Transform aniTarget)
    {

        yield return new WaitForSeconds(0.5f);

        Vector3 destPos = aniTarget.position;
        Vector3 startPos = go.transform.position;
        Vector3 nextPos;
        Vector3 currScale = go.transform.localScale;
        Transform goT = go.transform;

        float totalT = 0.3f;
        float currT = 0.0f;
        while(true)
        {
            float amount = currT / totalT;
            nextPos = Vector3.Lerp(startPos, destPos, amount);
            goT.position = nextPos;
            if (amount >= 1.0f) break;
            yield return new WaitForEndOfFrame();
            currT += Time.deltaTime;

            goT.localScale = (currScale * 0.95f);
            currScale = goT.localScale;

        }



        Destroy(go);


        SpriteRenderer sr = aniTarget.GetComponent<SpriteRenderer>();
        Color32 c = sr.color;
        c.a = 255;
        sr.color = c;



    }


}
