using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySecondGame;

public class UserGUI : MonoBehaviour
{
    public float timer = 120;
    private IUserController action;
    public int status = 0;
    GUIStyle style;
    GUIStyle buttonStyle;

    void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserController;

        style = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;

        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 30;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(90, 0, 80, 80), "Sphere is Priest.\nCube is Devil.", style);
        if(status == 0)
        {
            timer -= Time.deltaTime;
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 100, 50, 50), timer.ToString(), style);
        }
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                action.restart();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                action.restart();
            }
        }

        if(GUI.Button(new Rect(Screen.width/2 - 30, Screen.height/2 - 50, 100, 50), "Note!"))
        {
            action.moveAI();
        }

        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 300, 300, 300), action.getMinPlan());
    }
}