using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge: MonoBehaviour
{

    
    

    public void CanSet(int[,] GameBoard, int whiteorblack, ref int turn, int PresentX, int PresentY)
    {
        int LineMax = 8;
        bool judge = false;
        int[,] wheretogo = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { 1, 1 }, { -1, -1 }, { 1, -1 }, { -1, 1 } };

        if (GameBoard[PresentY, PresentX] != 1 && GameBoard[PresentY, PresentX] != 2)
        {
            for (int i = 0; i < 8; i++)
            {
                int r = PresentX + wheretogo[i, 0];
                int c = PresentY + wheretogo[i, 1];

                while (r >= 0 && r < LineMax && c >= 0 && c < LineMax && GameBoard[c, r] == (whiteorblack == 1 ? 2 : 1))
                {
                    r += wheretogo[i, 0];
                    c += wheretogo[i, 1];
                    if (r >= 0 && r < 8 && c >= 0 && c < 8 && GameBoard[c, r] == whiteorblack)
                    {
                        judge = true;
                        while (r != PresentX || c != PresentY)
                        {
                            r -= wheretogo[i, 0];
                            c -= wheretogo[i, 1];
                            GameBoard[c, r] = whiteorblack;
                        }
                        
                    }
                }
                
            }
        }

        if (judge)
        {
            GameBoard[PresentY, PresentX] = whiteorblack;
        }
        else
        {
            turn = turn - 1;
        }
    }

    public void Lignt(int[,] GameBoard, int whiteorblack, ref int turn, ref bool endjudge) 
    {
        int LineMax = 8;
        int[,] wheretogo = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { 1, 1 }, { -1, -1 }, { 1, -1 }, { -1, 1 } };
        bool canset = false;
        bool checkcanplace = false;

        for (int i = 0; i < LineMax; i++)
        {
            for (int j = 0; j < LineMax; j++)
            {
                if (GameBoard[j, i] != 1 && GameBoard[j, i] != 2)
                {
                    canset = false;
                    for (int k = 0; k < 8; k++)
                    {
                        int r = i + wheretogo[k, 0];
                        int c = j + wheretogo[k, 1];
                        while (r >= 0 && r < LineMax && c >= 0 && c < LineMax && GameBoard[c, r] == (whiteorblack == 1 ? 2 : 1))
                        {
                            r += wheretogo[k, 0];
                            c += wheretogo[k, 1];
                            if (r >= 0 && r < LineMax && c >= 0 && c < LineMax && GameBoard[c, r] == whiteorblack)
                            {
                                canset = true;
                                break;
                            }
                        }
                        if (canset)
                        {
                            break;
                        }
                    }
                    if (canset)
                    {
                        GameBoard[j, i] = 3;
                    }
                    else
                    {
                        GameBoard[j, i] = 0;
                    }
                }
            }
        }
        for (int i = 0; i < LineMax; i++)
        {
            for (int j = 0; j < LineMax; j++)
            {
                if (GameBoard[j, i] == 3)
                {
                    checkcanplace = true;
                }
            }
        }
        if (!checkcanplace)
        {
            turn--;
            whiteorblack = turn % 2 + 1;

            for (int i = 0; i < LineMax; i++)
            {
                for (int j = 0; j < LineMax; j++)
                {
                    if (GameBoard[j, i] != 1 && GameBoard[j, i] != 2)
                    {
                        canset = false;
                        for (int k = 0; k < 8; k++)
                        {
                            int r = i + wheretogo[k, 0];
                            int c = j + wheretogo[k, 1];
                            while (r >= 0 && r < LineMax && c >= 0 && c < LineMax && GameBoard[c, r] == (whiteorblack == 1 ? 2 : 1))
                            {
                                r += wheretogo[k, 0];
                                c += wheretogo[k, 1];
                                if (r >= 0 && r < LineMax && c >= 0 && c < LineMax && GameBoard[c, r] == whiteorblack)
                                {
                                    canset = true;
                                    break;
                                }
                            }
                            if (canset)
                            {
                                break;
                            }
                        }
                        if (canset)
                        {
                            GameBoard[j, i] = 3;
                        }
                        else
                        {
                            GameBoard[j, i] = 0;
                        }
                    }
                }
            }
            for (int i = 0; i < LineMax; i++)
            {
                for (int j = 0; j < LineMax; j++)
                {
                    if (GameBoard[j, i] == 3)
                    {
                        checkcanplace = true;
                    }
                }
            }
            if (!checkcanplace)
            {
                endjudge = true;
            }

            }
        }
}
