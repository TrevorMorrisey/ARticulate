using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagButton : MonoBehaviour
{
    public TagButtonManager tabButtonManager;
    public int index;
    public Image buttonBackground;
    private bool isActive;

    public void Press()
    {
        isActive = !isActive;

        if (isActive)
        {
            buttonBackground.color = tabButtonManager.activeColor;
        }
        else
        {
            buttonBackground.color = tabButtonManager.inactiveColor;
        }
    }

}