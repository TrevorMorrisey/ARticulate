using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;

public class DrawableHelper : MonoBehaviour
{
    //public bool editModeActive;
    public Drawable drawable;
    //public Transform quad;

    private void Update()
    {
        //print(Input.mousePosition);
        //if (!editModeActive)
        //{
        //    return;
        //}

        //if (Input.mousePosition.y > 2350f || Input.mousePosition.y < 835f)
        if (Input.mousePosition.y > 2200f || Input.mousePosition.y < 850f)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            SendRay();
        }
        //else
        //{
        //    print("Mouse not clicked");
        //}
    }

    private void SendRay()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 6))
        //{
        //    //print("Drawable clicked");
        //    //drawable.PenBrush(hit.point / 0.025f);
        //    //drawable.PenBrush(hit.point);
        //    Vector3 localPos = quad.InverseTransformPoint(hit.point);
        //    //print(localPos.ToString());
        //    //float xPercentage = (localPos.x + 0.125f) / 0.25f;
        //    //float yPercentage = (localPos.y + 0.125f) / 0.25f;
        //    float xPercentage = (localPos.x + 0.25f) / 0.5f;
        //    float yPercentage = (localPos.y + 0.25f) / 0.5f;

        //    //drawable.PenBrush(new Vector2(xPercentage, yPercentage) * 1000f);
        //    drawable.PenBrush(new Vector2(xPercentage, yPercentage) * 1000f);
        //    //xPercentage /= 0.25f;
        //    //print("xPercentage: " + xPercentage);
        //    //print("X Position: " + localPos.x);
        //    //0.025
        //}

        //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 60f);
        //print(Input.mousePosition);
        //float xPercentage = Input.mousePosition.x / Screen.width;
        //float yPercentage = Input.mousePosition.y / Screen.height;
        Vector2 inputPixelPosition = new Vector2(Input.mousePosition.x - (Screen.width / 2f), Input.mousePosition.y - (Screen.height / 2f));
        Vector2 drawableAreaPixelPosition = inputPixelPosition - new Vector2(-1426f / 2f, (-1920f / 2f) + 350f); //-1427f
        //Vector2 pixelPosition = new Vector2(xPercentage * 1427f, yPercentage * 1920f);
        //print(drawableAreaPixelPosition);
        drawable.PenBrush(drawableAreaPixelPosition);
    }

}