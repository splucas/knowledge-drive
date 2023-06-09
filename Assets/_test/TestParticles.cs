using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticles : MonoBehaviour
{

    
    public Transform CorrectAnswerTarg;

    public GameObject CorrectAnswers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject go = Instantiate(CorrectAnswers);
        AnswerCorrectController acc = go.GetComponent<AnswerCorrectController>();
        acc.ShowCorrect(CorrectAnswerTarg, Vector3.zero);
    }
}
