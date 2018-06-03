using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGuiHealth : MonoBehaviour {

    public float hSliderValue = 0.0F;
    public Transform transform;


    void OnGUI()
    {
        /*GUIStyle sliderStyle = new GUIStyle();
        sliderStyle.normal.background.width = 200;
        sliderStyle.normal.background.height = 50;
        sliderStyle.normal.textColor = Color.red;*/
       // sliderStyle.normal.background.LoadImage
        

        //sliderStyle.border.Add(new Rect(transform.position.x * 55 + Screen.width / 2 - 50, Screen.height / 2 - 100 - transform.position.y * 5, 100, 30));
        
        hSliderValue = GUI.HorizontalSlider(new Rect(transform.position.x * 55 + Screen.width/2 - 50, Screen.height/2 - 100 - transform.position.y * 5, 100, 30), hSliderValue, 0.0F, 10.0F);
    }
}
