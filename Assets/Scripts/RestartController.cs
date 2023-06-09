using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartController : MonoBehaviour
{

    public TextMeshProUGUI CorrectCount;
    public TextMeshProUGUI WrongCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScore(int correct, int wrong)
    {
        CorrectCount.text = correct.ToString();
        WrongCount.text = wrong.ToString();


    }
}
