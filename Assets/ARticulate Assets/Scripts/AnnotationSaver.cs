using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using FreeDraw;
using UnityEngine.UI;
using System;

// This implementation worked on PC but not on Android. Didn't have time to fix
public class AnnotationSaver : MonoBehaviour
{
    public Drawable drawArea;
    public string folderName;

    public void SaveAnnotation(int currentPaintingID)
    {
        //Sprite sprite = drawArea.drawable_sprite.sprite;
        //Texture2D texture = sprite.texture;
        //byte[] textureBytes = texture.EncodeToPNG();
        //string filePath = Path.Combine(Application.streamingAssetsPath, folderName, "painting" + currentPaintingID.ToString() + ".png");
        //File.WriteAllBytes(filePath, textureBytes);
    }

}