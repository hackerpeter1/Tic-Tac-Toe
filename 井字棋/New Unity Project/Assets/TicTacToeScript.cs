using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeScript : MonoBehaviour {
    private int turn = 1;   //轮到谁
    private int[,] state = new int[3, 3];    //二位数组记录状态  
    int num = 0;
    
    void Start()
    {
        reset();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(130, 20, 100, 50), "Player1:O");
        GUI.Label(new Rect(130, 40, 100, 50), "Player1:X");
        GUI.Label(new Rect(130, 60, 100, 50), "Player1 First!");
        if (GUI.Button(new Rect(300, 300, 100, 50), "Reset"))
            reset();
        int result = check();
        if (result == 1)
        {
            GUI.Label(new Rect(300, 230, 100, 100), "Player1 wins!");
        }
        else if (result == 2)
        {
            GUI.Label(new Rect(275, 230, 100, 100), "Player2 wins!");
        }else if (result == 3)
        {
            GUI.Label(new Rect(275, 230, 150, 100), "The Game Is Drawn!");
        }
        load();
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (GUI.Button(new Rect(275 + i * 50, j * 50, 50, 50), ""))
                {
                    if (result == 0)
                    {
                        num++;
                        if (turn == 1)
                            state[i, j] = 1;
                        else
                            state[i, j] = 2;
                        turn = -turn;
                    }
                }
            }
        }
    }

    void load()
    {
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (state[i, j] == 1)
                    GUI.Button(new Rect(275 + i * 50, j * 50, 50, 50), "O");   //加载之前的状态

                if (state[i, j] == 2)
                    GUI.Button(new Rect(275 + i * 50, j * 50, 50, 50), "X");
            }
        }
    }

    int check()
    {
        for (int j = 0; j < 3; ++j)
        {
            if (state[0, j] != 0 && state[0, j] == state[1, j] && state[1, j] == state[2, j])
            {
                return state[0, j];    //横排
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            if (state[i, 0] != 0 && state[i, 0] == state[i, 1] && state[i, 1] == state[i, 2])
            {
                return state[i, 0];    //纵排
            }
        }
        if (state[1, 1] != 0 &&
            state[0, 0] == state[1, 1] && state[1, 1] == state[2, 2] ||
            state[0, 2] == state[1, 1] && state[1, 1] == state[2, 0])
        {
            return state[1, 1];        //对角线
        }
        if (num == 9)
        {
            return 3;       //平局情况
        }
        return 0;
    }

    void reset()
    {
        num = 0;
        turn = 1;
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                state[i, j] = 0;
            }
        }
    }
}
