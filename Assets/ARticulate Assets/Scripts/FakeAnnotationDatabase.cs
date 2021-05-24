using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This would ideally just be a 2D array of Annotation but Unity doesn't allow 2D arrays to be accessed in the editor
// To get around that I created a separate class, AnnotationSet, that just wraps an Annotation array
public class FakeAnnotationDatabase : MonoBehaviour
{
    public AnnotationSet[] paintings;

}