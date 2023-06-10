using System;
using UnityEngine;
using TMPro;

public class PlayerLogic : MonoBehaviour
{
    public TextMeshProUGUI TMPText;

    public event Action<GameObject> OnCollision;

    MatchItem matchData;

    public MatchItem MatchItemData { get { return matchData; } }


    private void Awake()
    {
        TMPText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMatchItemData(MatchItem item)
    {

        matchData = item;
        TMPText.text = item.Topic;
    }

    private void OnCollisionEnter2D(Collision2D collision) => OnCollision.Invoke(collision.gameObject);
    
}
