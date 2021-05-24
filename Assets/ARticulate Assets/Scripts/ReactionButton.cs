using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionButton : MonoBehaviour
{
    private bool reactionMenuOpen;
    public GameObject reactionMenu;

    public void PressReact()
    {
        reactionMenuOpen = !reactionMenuOpen;

        reactionMenu.SetActive(reactionMenuOpen);
    }

    public void PressReactionButton(int index)
    {
        //print("Reacted " + index + " to painting one");
        CloseResponseUI();
    }

    public void CloseResponseUI()
    {
        reactionMenuOpen = false;

        reactionMenu.SetActive(false);
    }

}