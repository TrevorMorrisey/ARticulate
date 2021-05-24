using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingDetector : MonoBehaviour
{
    public GameManager gameManager;
    public int paintingID;

    private void OnEnable()
    {
        gameManager.SetActivePainting(paintingID);
    }

    private void OnDisable()
    {
        gameManager.SetNoPaintingActive();
    }

}