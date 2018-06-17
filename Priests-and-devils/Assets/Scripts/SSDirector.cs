using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MySecondGame
{
    public class SSDirector : System.Object
    {
        private static SSDirector _instance;
        public ISceneController currentSceneController { get; set; }

        public static SSDirector getInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }
    }

    public interface ISceneController
    {
        void loadResources();
    }

    public interface IUserController
    {
        void moveBoat();
        void characterIsClicked(MyCharacterController characterCtrl);
        void restart();

        //AI智能
        bool moveAI();
        string getMinPlan();
    }
}

