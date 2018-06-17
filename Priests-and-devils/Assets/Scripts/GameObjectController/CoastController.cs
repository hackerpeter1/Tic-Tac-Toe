using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySecondGame
{
    public class CoastController
    {
        readonly GameObject coast;
        readonly Vector3 from_pos = new Vector3(9, 1, 0);               //右边岸的位置
        readonly Vector3 to_pos = new Vector3(-9, 1, 0);                //左边岸边的位置
        readonly Vector3[] positions;                                   //存储角色的初始化位置
        readonly int to_or_from;    // to->-1, from->1

        // change frequently
        MyCharacterController[] passengerPlaner;                        //存储角色的位置实例

        public CoastController(string _to_or_from)
        {
            positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
                new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};

            passengerPlaner = new MyCharacterController[6];

            if (_to_or_from == "from")
            {
                coast = Object.Instantiate(Resources.Load("Prefabs/Riverside", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
                coast.name = "from";
                to_or_from = 1;
            }
            else
            {
                coast = Object.Instantiate(Resources.Load("Prefabs/Riverside", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
                coast.name = "to";
                to_or_from = -1;
            }
        }

        public int getEmptyIndex()                                          //获取当前岸边缺省的第一个位置
        {
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public Vector3 getEmptyPosition()                                   //获取当前岸边缺省位置第一个位置的transform
        {
            Vector3 pos = positions[getEmptyIndex()];
            pos.x *= to_or_from;
            return pos;
        }

        public void getOnCoast(MyCharacterController characterCtrl)             //角色上岸
        {
            int index = getEmptyIndex();
            passengerPlaner[index] = characterCtrl;
        }

        public MyCharacterController getOffCoast(string passenger_name)             //角色离开岸，返回离开岸边的角色
        {   // 0->priest, 1->devil
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] != null && passengerPlaner[i].getName() == passenger_name)
                {
                    MyCharacterController charactorCtrl = passengerPlaner[i];
                    passengerPlaner[i] = null;
                    return charactorCtrl;
                }
            }
            Debug.Log("cant find passenger on coast: " + passenger_name);
            return null;
        }

        public int get_to_or_from()                 //获取是哪个岸
        {
            return to_or_from;
        }  

        public int[] getCharacterNum()              //获取当前岸上的所有角色的数量
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] == null)
                    continue;
                if (passengerPlaner[i].getType() == 0)
                {   // 0->priest, 1->devil
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }

        public void reset()                         //重置岸
        {
            passengerPlaner = new MyCharacterController[6];
        }

        public MyCharacterController getAPriest()
        {
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i].getType() == 0 && passengerPlaner[i] != null)
                {
                    return passengerPlaner[i];
                }
            }
            return null;
        }

        public MyCharacterController getADevil()
        {
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] != null && passengerPlaner[i].getType() == 1)
                {
                    return passengerPlaner[i];
                }
            }

            return null;
        }
    }
}