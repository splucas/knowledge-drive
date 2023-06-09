using System;
using System.Collections.Generic;
using UnityEngine;


public struct MatchItem
{
    public string Topic;
    public string Attribute;
    public bool MatchThis;
}


[CreateAssetMenu(fileName = "MatchableData", menuName = "Game Data/Make Match Data")]
public class MatchData : ScriptableObject
{

    [Serializable]
    public class MatchStuff
    {
        public string Topic;
        public List<string> Attributes;
        
            
    }

    public List<MatchStuff> MatchItems;

    public void GetRandomMatchables(MatchItem[] itemBuffer, string lastTopic)
    {
        int cnt = itemBuffer.Length;

        // Create a copy
        List<MatchStuff> listCopy = new List<MatchStuff>(MatchItems);
        System.Random randNum = new System.Random();

        int randNdx;
        MatchStuff randoItem = null;
        bool isOkay = false;
        int maxCheck = 0;
        while( !isOkay && maxCheck < 10)
        {
            randNdx = randNum.Next(0, listCopy.Count);
            // Grab a random element (that becomes the match element)
            randoItem = listCopy[randNdx];
            isOkay = randoItem.Topic != lastTopic;
            maxCheck += 1;

        }

        listCopy.Remove(randoItem);
        itemBuffer[0] = new MatchItem()
        {
            MatchThis = true,
            Topic = randoItem.Topic,
            Attribute = randoItem.Attributes[randNum.Next(0, randoItem.Attributes.Count)]
        };

        // Grab random values from the other two options
        for(int ndx = 0; ndx < listCopy.Count; ndx ++)
        {
            //randNdx = randNum.Next(0, listCopy.Count);
            randoItem = listCopy[ndx];
            itemBuffer[ndx+1] = new MatchItem()
            {
                MatchThis = false,
                Topic = randoItem.Topic,
                Attribute = randoItem.Attributes[randNum.Next(0, randoItem.Attributes.Count)]
            };
        }

        // Shuffle buffer:
        for ( cnt = itemBuffer.Length - 1; cnt > 0; --cnt)
        {
            int rndint = randNum.Next(cnt + 1);
            MatchItem temp = itemBuffer[cnt];
            itemBuffer[cnt] = itemBuffer[rndint];
            itemBuffer[rndint] = temp;
        }


    }





}
