using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MySecondGame
{
    public class CCActionManager : SSActionManager, ISSActionCallback
    {
        public FirstController sceneController;
        protected void Start()
        {
            sceneController = (FirstController)SSDirector.getInstance().currentSceneController;
            sceneController.actionManager = this;
        }

        public void moveBoat(BoatController boat)
        {
            CCMoveToAction action = CCMoveToAction.GetSSAction(boat.getDestination(), boat.movingSpeed);
            this.RunAction(boat.getGameobj(), action, this);
        }

        public void moveCharacter(MyCharacterController characterCtrl, Vector3 destination)
        {
            Vector3 currentPos = characterCtrl.getPos();
            List<SSAction> action_list = new List<SSAction>();
            
            if(destination.y < currentPos.y)
            {
                Vector3 middlePos = new Vector3() { x = destination.x, y = currentPos.y, z = destination.z };
                SSAction action1 = CCMoveToAction.GetSSAction(middlePos, characterCtrl.movingSpeed);
                SSAction action2 = CCMoveToAction.GetSSAction(destination, characterCtrl.movingSpeed);

                action_list.Add(action1);
                action_list.Add(action2);
                SSAction seqAction = CCSequenceAction.GetSSAction(1, 0, action_list);
                this.RunAction(characterCtrl.getGameObj(), seqAction, this);
            }
            else if( destination.y > currentPos.y)
            {
                Vector3 middlePos = new Vector3() { x = currentPos.x, y = destination.y, z = currentPos.z };
                SSAction action1 = CCMoveToAction.GetSSAction(middlePos, characterCtrl.movingSpeed);
                SSAction action2 = CCMoveToAction.GetSSAction(destination, characterCtrl.movingSpeed);

                action_list.Add(action1);
                action_list.Add(action2);
                SSAction seqAction = CCSequenceAction.GetSSAction(1, 0, action_list);
                this.RunAction(characterCtrl.getGameObj(), seqAction, this);
            }
        }

        public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
        {
           //
        }
    }
}