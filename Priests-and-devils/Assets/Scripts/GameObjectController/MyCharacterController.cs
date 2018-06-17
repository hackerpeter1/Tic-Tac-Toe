using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySecondGame
{
    public class MyCharacterController
    {
        public readonly float movingSpeed = 20;
        readonly GameObject character;
        //readonly Moveable moveableScript;
        readonly ClickGUI clickGUI;
        readonly int characterType; // 0->priest, 1->devil

        // change frequently
        bool _isOnBoat;
        CoastController coastController;


        public MyCharacterController(string which_character)
        {
            if (which_character == "priest")
            {
                character = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 0;
            }
            else
            {
                character = Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 1;
            }
            //moveableScript = character.AddComponent(typeof(Moveable)) as Moveable;

            clickGUI = character.AddComponent(typeof(ClickGUI)) as ClickGUI;      //创建点击事件
            clickGUI.setController(this);
        }

        public GameObject getGameObj()
        {
            return character;
        }

        public void setName(string name)
        {
            character.name = name;
        }

        public void setPosition(Vector3 pos)            //设置游戏对象位置
        {
            character.transform.position = pos;
        }

        /*public void moveToPosition(Vector3 destination)     //设置移动位置
        {
            //moveableScript.setDestination(destination);
        }*/

        public int getType()                                //获取游戏对象类型
        {   // 0->priest, 1->devil
            return characterType;
        }

        public string getName()                             //获取角色名字
        {
            return character.name;
        }

        public void getOnBoat(BoatController boatCtrl)              //移动到船之后进行和船的绑定
        {
            coastController = null;
            character.transform.parent = boatCtrl.getGameobj().transform;
            _isOnBoat = true;
        }

        public void getOnCoast(CoastController coastCtrl)            //进行和岸边的绑定
        {
            coastController = coastCtrl;
            character.transform.parent = null;
            _isOnBoat = false;
        }

        public bool isOnBoat()                                      //返回游戏对象是否在船上
        {
            return _isOnBoat;
        }

        public CoastController getCoastController()                    //获取岸边的控制器
        {
            return coastController;
        }

        public Vector3 getPos()
        {
            return this.character.transform.position;
        }

        public void reset()                                             //重置游戏对象的设置
        {
            //moveableScript.reset(); 
            coastController = (SSDirector.getInstance().currentSceneController as FirstController).fromCoast;
            getOnCoast(coastController);
            setPosition(coastController.getEmptyPosition());
            coastController.getOnCoast(this);
        }
    }
}
