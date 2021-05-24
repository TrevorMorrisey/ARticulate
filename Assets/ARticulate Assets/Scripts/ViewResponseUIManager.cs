using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewResponseUIManager : MonoBehaviour
{
    public Image topReactionImageOne;
    public Image topReactionImageTwo;

    public Image allReactionsImageOne;
    public Image allReactionsImageTwo;
    public Image allReactionsImageThree;
    public Image allReactionsImageFour;
    public Image allReactionsImageFive;

    public TMP_Text allReactionsValueOne;
    public TMP_Text allReactionsValueTwo;
    public TMP_Text allReactionsValueThree;
    public TMP_Text allReactionsValueFour;
    public TMP_Text allReactionsValueFive;

    public Sprite[] reactionSprites;

    private class Reaction
    {
        public int index;
        public int count;
    }

    public void SetResponseCount(int[] responseCounts)
    {
        List<Reaction> reactions = new List<Reaction>();

        for (int i = 0; i < responseCounts.Length; i++)
        {
            Reaction reaction = new Reaction();
            reaction.index = i;
            reaction.count = responseCounts[i];
            reactions.Add(reaction);
        }

        // https://answers.unity.com/questions/855088/sorting-a-list-of-gameobjects-by-accessing-their-i.html
        reactions.Sort(delegate (Reaction a, Reaction b) {
            return (a.count).CompareTo(b.count);
        });

        reactions.Reverse();


        topReactionImageOne.sprite = reactionSprites[reactions[0].index];
        topReactionImageTwo.sprite = reactionSprites[reactions[1].index];

        allReactionsImageOne.sprite = reactionSprites[reactions[0].index];
        allReactionsImageTwo.sprite = reactionSprites[reactions[1].index];
        allReactionsImageThree.sprite = reactionSprites[reactions[2].index];
        allReactionsImageFour.sprite = reactionSprites[reactions[3].index];
        allReactionsImageFive.sprite = reactionSprites[reactions[4].index];

        allReactionsValueOne.text = reactions[0].count.ToString();
        allReactionsValueTwo.text = reactions[1].count.ToString();
        allReactionsValueThree.text = reactions[2].count.ToString();
        allReactionsValueFour.text = reactions[3].count.ToString();
        allReactionsValueFive.text = reactions[4].count.ToString();
    }

}