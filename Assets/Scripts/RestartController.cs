using UnityEngine;
using TMPro;

public class RestartController : MonoBehaviour
{

    public TextMeshProUGUI CorrectCount;
    public TextMeshProUGUI WrongCount;

    public void ShowScore(int correct, int wrong)
    {
        CorrectCount.text = correct.ToString();
        WrongCount.text = wrong.ToString();

    }
}
