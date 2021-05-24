using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;

public class ColorButton : MonoBehaviour
{
    public Color color;

    public void SetActiveColor()
    {
        Drawable.Pen_Colour = color;
    }

    public void SetPenWidth(bool large)
    {
        if (large)
        {
            Drawable.Pen_Width = 25;
        }
        else
        {
            Drawable.Pen_Width = 6;
        }
    }

}