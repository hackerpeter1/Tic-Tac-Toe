using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySecondGame;

public class ClickGUI : MonoBehaviour
{
    IUserController action;
    MyCharacterController characterController;

    public void setController(MyCharacterController characterCtrl)
    {
        characterController = characterCtrl;
    }

    void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserController;
    }


    void OnMouseDown()
    {
        if (gameObject.name == "boat")
        {
            action.moveBoat();
        }
        else
        {
            action.characterIsClicked(characterController);
        }
    }
}
