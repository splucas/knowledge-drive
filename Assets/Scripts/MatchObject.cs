using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchObject : MonoBehaviour
{

    public TextMeshProUGUI TMPText;

    public string MatchText;

    MatchItem matchItemData;

    public MatchItem MatchItemData {get {return matchItemData; }}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMatchItemData(MatchItem matchItem)
    {
        matchItemData = matchItem;
        MatchText = matchItem.Topic;
        TMPText.text = matchItem.Attribute;
    }


}
