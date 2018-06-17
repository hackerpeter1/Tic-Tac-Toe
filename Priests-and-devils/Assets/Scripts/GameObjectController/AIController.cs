using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySecondGame   
{
    /*
     * 在这里维护图的数据结构
     */
    public class AIController
    {   
        //边（船的行走信息）
        public enum edge { P, D, PP, DD, PD }
        //点（岸边的状态，B表示船，P表示牧师，D表示魔鬼）
        public enum point { P3D3B, P2D2, P3D2, P3D1, P3D2B,
                            P3D1B, P3D0, P1D1, P2D2B, P0D2,
                            P0D3B, P0D1, P2D1B, P1D1B, P0D2B,
                            P0D0, UNDONE}
        //邻接矩阵表示方法无向图
        public int[,] graph = new int[16,16];
        //标记数组
        public int[] sign_arr = new int[16];

        public AIController()
        {
            //初始化图，没有连线的都是-1
            for(int i = 0; i < 16; i++)
            {
                for(int j = 0; j < 16; j++)
                {
                    graph[i, j] = -1;
                }
            }
            //连线过程
            //P3D3B
            graph[(int)point.P3D3B, (int)point.P3D2] = (int)edge.D;
            graph[(int)point.P3D2, (int)point.P3D3B] = (int)edge.D;
            graph[(int)point.P3D3B, (int)point.P3D1] = (int)edge.DD;
            graph[(int)point.P3D1, (int)point.P3D3B] = (int)edge.DD;
            graph[(int)point.P3D3B, (int)point.P2D2] = (int)edge.PD;
            graph[(int)point.P2D2, (int)point.P3D3B] = (int)edge.PD;
            //P2D2
            graph[(int)point.P2D2, (int)point.P3D2B] = (int)edge.P;
            graph[(int)point.P3D2B, (int)point.P2D2] = (int)edge.P;
            //P3D2B
            graph[(int)point.P3D2B, (int)point.P3D1] = (int)edge.D;
            graph[(int)point.P3D1, (int)point.P3D2B] = (int)edge.D;
            graph[(int)point.P3D2B, (int)point.P3D0] = (int)edge.DD;
            graph[(int)point.P3D0, (int)point.P3D2B] = (int)edge.DD;
            //P30D
            graph[(int)point.P3D0, (int)point.P3D1B] = (int)edge.D;
            graph[(int)point.P3D1B, (int)point.P3D0] = (int)edge.D;
            //P3D1B
            graph[(int)point.P3D1B, (int)point.P1D1] = (int)edge.PP;
            graph[(int)point.P1D1, (int)point.P3D1B] = (int)edge.PP;
            //P1D1
            graph[(int)point.P1D1, (int)point.P2D2B] = (int)edge.PD;
            graph[(int)point.P2D2B, (int)point.P1D1] = (int)edge.PD;
            //P2D2B
            graph[(int)point.P2D2B, (int)point.P0D2] = (int)edge.PP;
            graph[(int)point.P0D2, (int)point.P2D2B] = (int)edge.PP;
            //P0D2
            graph[(int)point.P0D2, (int)point.P0D3B] = (int)edge.D;
            graph[(int)point.P0D3B, (int)point.P0D2] = (int)edge.D;
            //P0D3B
            graph[(int)point.P0D3B, (int)point.P0D1] = (int)edge.DD;
            graph[(int)point.P0D1, (int)point.P0D3B] = (int)edge.DD;
            //P0D1
            graph[(int)point.P0D1, (int)point.P2D1B] = (int)edge.PP;
            graph[(int)point.P2D1B, (int)point.P0D1] = (int)edge.PP;
            graph[(int)point.P0D1, (int)point.P1D1B] = (int)edge.P;
            graph[(int)point.P1D1B, (int)point.P0D1] = (int)edge.P;
            graph[(int)point.P0D1, (int)point.P0D2B] = (int)edge.D;
            graph[(int)point.P0D2B, (int)point.P0D1] = (int)edge.D;
            //P1D1B
            graph[(int)point.P1D1B, (int)point.P0D0] = (int)edge.PD;
            graph[(int)point.P0D0, (int)point.P1D1B] = (int)edge.PD;
            //P0D2B
            graph[(int)point.P0D2B, (int)point.P0D0] = (int)edge.DD;
            graph[(int)point.P0D0, (int)point.P0D2B] = (int)edge.DD;
        }

        public int[] getMovePlan(point curPoint)
        {
            if(curPoint == point.UNDONE)
            {
                return null;
            }
            //初始化标记矩阵
            for (int i = 0; i < 16; i++)
            {
                sign_arr[i] = 0;
            }
            Stack<int> path = new Stack<int>();
            int[,] result = new int[16,2];     //存储了curPoint到其他节点的最短距离
            for(int i = 0; i < 16; i++)     //初始化result数组
            {
                result[i,0] = 999999;
            }
            
            for(int i = 0; i < 16; i++)     //更新result
            {
                if(graph[(int)curPoint, i] > -1 && i != (int)curPoint)
                {
                    result[i,0] = 1;
                    result[i, 1] = (int)curPoint;
                }
            }

            sign_arr[(int)curPoint] = 1;      //标记curPoint
            result[(int)curPoint,0] = 0;      //到自己的距离是0

            int time = 10;
            while (!arrAllSign())
            {
                int minPosition = getMinPositionInArr(result);  //找到最小的位置

                //将最小的点能到达的点更新到result
                for(int i = 0; i < 16; i++)
                {
                    if(graph[minPosition, i] > -1 && sign_arr[i] != 1 && minPosition != i)
                    {
                        if(result[minPosition,0] + 1 < result[i,0])  //当当前点到点i小于前一点到点i的距离时更新
                        {
                            result[i, 0] = result[minPosition,0] + 1;
                            result[i, 1] = minPosition;
                        }
                    }
                }

                sign_arr[minPosition] = 1;                       //将最小点设置为已标记
                time--;
            }

            //获取路径
            Stack<int> Path = new Stack<int>();
            Path.Push(15);
            Path.Push(result[15,1]);
            while (Path.Peek() != (int)curPoint)
            {
                Path.Push(result[Path.Peek(), 1]);
            }

            int[] Plan = new int[Path.Count - 1];
            int[] path_arr = Path.ToArray();
            for(int i = 0; i < Path.Count - 1; i++)
            {
                //Debug.Log(graph[path_arr[i], path_arr[i + 1]]);
                Plan[i] = graph[path_arr[i], path_arr[i + 1]];
            }

            return Plan;
        }

        private bool arrAllSign()
        {
            for(int i = 0; i < 16; i++)
            {
                if(sign_arr[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private int getMinPositionInArr(int[,] arr)
        {
            int smallest = 999999;
            int position = 0;

            for (int i = 0; i < 16; i++)
            {
                if ( (sign_arr[i] != 1) && (arr[i,0] < smallest) )   //没有被标记的点
                {
                    smallest = arr[i,0];
                    position = i;
                }
            }
            return position;
        }
    }
}