using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Annotation
{
    public Sprite visualAnnotation;
    public string textAnnotation;
    public string author;
    public string date;
    public int[] reactionCounts = new int[5];
}