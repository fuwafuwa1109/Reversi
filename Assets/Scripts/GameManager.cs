using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[,] BoardObj = new GameObject[8, 8];
    private GameObject[,] EmptyObj = new GameObject[8, 8];
    public GameObject Empty;
    public GameObject White;
    public GameObject Black;
    public GameObject LightWhite;
    public GameObject LightBlack;
    private int[,] GameBoard = new int[8,8];
    private int[,] StartBoard = new int[8,8]{
        {0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 2, 1, 0, 0, 0 },
        {0, 0, 0, 1, 2, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0 },
    };
    private int LineMax = 8;
    private int PresentPosX;
    private int PresentPosY;
    private Judge judge;
    private int whiteorblack;
    private int turn;
    private int countWhite = 0;
    private int countBlack = 0;
    private bool endjudge = false;


    public Button retrybutton;
    public Button titlebutton;
    public Text WhiteText;
    public Text BlackText;
    public Text Whitepoint;
    public Text Blackpoint;
    public Text WhiteWin;
    public Text BlackWin;
    public Text currentWhite;
    public Text currentBlack;
    public Text Draw;

    private bool menucheck = true;
    void Start()
    {
        judge = gameObject.AddComponent<Judge>();
        turn = 1;
        whiteorblack = turn % 2 + 1;
        for (int i = 0; i < LineMax; i++)
        {
            for (int j = 0; j < LineMax; j++)
            {
                EmptyObj[j, i] = Instantiate(Empty, new Vector3((float)i + 0.5f, (float)j + 0.5f, 0), Quaternion.identity);
            }
        }
        for (int i = 0; i < LineMax; i++)
        {
            for (int j = 0; j < LineMax; j++)
            {
                GameBoard[j, i] = StartBoard[j, i];
            }
        }
        judge.Lignt(GameBoard, whiteorblack, ref turn, ref endjudge);
        BoardUpdate();

        Material materialempty = Empty.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
        Material materialwhite = White.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
        Material materialblack = Black.GetComponentInChildren<SpriteRenderer>().sharedMaterial;

        Color color = materialempty.color;
        color.a = 1.0f;
        materialempty.color = color;

        Color colorwhite = materialwhite.color;
        color.a = 1.0f;
        materialwhite.color = color;

        Color colorblack = materialblack.color;
        color.a = 1.0f;
        materialblack.color = color;
        menucheck = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Input.mousePosition; // マウスのスクリーン座標を取得
            mousePosition.z = 10.0f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // カメラのビューポート座標をワールド座標に変換
            if (worldPosition.x >= 0.0f && worldPosition.x < 8.0f &&
                worldPosition.y >= 0.0f && worldPosition.y < 8.0f)  
            {
                
                PresentPosX = Mathf.FloorToInt(worldPosition.x);
                PresentPosY = Mathf.FloorToInt(worldPosition.y);
                judge.CanSet(GameBoard, whiteorblack, ref turn, PresentPosX, 7 - PresentPosY);
                turn++;
                whiteorblack = turn % 2 + 1;
            }
            judge.Lignt(GameBoard, whiteorblack, ref turn, ref endjudge);
            whiteorblack = turn % 2 + 1;
            BoardUpdate();
        }
        countWhite = 0;
        countBlack = 0;

        for (int i = 0; i < LineMax; i++)
        {
            for (int j = 0; j < LineMax; j++)
            {
                if (GameBoard[j, i] == 1)
                {
                    countWhite++;
                }
                if (GameBoard[j, i] == 2)
                {
                    countBlack++;
                }
            }
        }
        currentWhite.text = "" + countWhite;
        currentBlack.text = "" + countBlack;




        if (countBlack + countWhite == 64 || countWhite == 0 || countBlack == 0 || endjudge )
        {
            Material materialempty = Empty.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
            Material materialwhite = White.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
            Material materialblack = Black.GetComponentInChildren<SpriteRenderer>().sharedMaterial;

            Color color = materialempty.color;
            color.a = 0.3f;
            materialempty.color = color;

            Color colorwhite = materialwhite.color;
            color.a = 0.3f;
            materialwhite.color = color;

            Color colorblack = materialblack.color;
            color.a = 0.3f;
            materialblack.color = color;


            retrybutton.gameObject.SetActive(true);
            titlebutton.gameObject.SetActive(true);
            WhiteText.gameObject.SetActive(true);
            BlackText.gameObject.SetActive(true);
            WhiteText.gameObject.SetActive(true);
            BlackText.gameObject.SetActive(true);
            Whitepoint.gameObject.SetActive(true);
            Blackpoint.gameObject.SetActive(true);
            Whitepoint.text = "     " + countWhite;
            Blackpoint.text = "   " + countBlack;
            if (countWhite > countBlack)
            {
                WhiteWin.gameObject.SetActive(true);
            }
            else if (countBlack > countWhite)
            {
                BlackWin.gameObject.SetActive(true);
            }
            else
            {
                Draw.gameObject.SetActive(true);
            }
            endjudge = false;
        }
    }


    void BoardUpdate()
    {      
        for (int j = 0; j < LineMax; j++)
        {
            for (int i = 0; i < LineMax; i++)
            {
                Destroy(BoardObj[j, i]);
                if (GameBoard[j, i] == 1)
                {
                    BoardObj[j, i] = Instantiate(White, new Vector3((float)i + 0.5f, 7.0f - (float)j + 0.5f, 0), Quaternion.identity);
                }
                if (GameBoard[j, i] == 2)
                {
                    BoardObj[j, i] = Instantiate(Black, new Vector3((float)i + 0.5f, 7.0f - (float)j + 0.5f, 0), Quaternion.identity);
                }
                if (GameBoard[j, i] == 3)
                {
                    if (whiteorblack == 1)
                    {
                        BoardObj[j, i] = Instantiate(LightWhite, new Vector3((float)i + 0.5f, 7.0f - (float)j + 0.5f, 0), Quaternion.identity);
                    }
                    else
                    {
                        BoardObj[j, i] = Instantiate(LightBlack, new Vector3((float)i + 0.5f, 7.0f - (float)j + 0.5f, 0), Quaternion.identity);
                    }
                }
                if (GameBoard[j, i] == 0)
                {
                    BoardObj[j, i] = Instantiate(Empty, new Vector3((float)i + 0.5f, 7.0f - (float)j + 0.5f, 0), Quaternion.identity);
                }
            }
        }
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        retrybutton.gameObject.SetActive(false);
        titlebutton.gameObject.SetActive(false);
        WhiteText.gameObject.SetActive(false);
        BlackText.gameObject.SetActive(false);
        WhiteText.gameObject.SetActive(false);
        BlackText.gameObject.SetActive(false);
        Whitepoint.gameObject.SetActive(false);
        Blackpoint.gameObject.SetActive(false);
        WhiteWin.gameObject.SetActive(false);
        BlackWin.gameObject.SetActive(false);
        Draw.gameObject.SetActive(false);
        turn = 1;
        whiteorblack = turn % 2 + 1;
        for (int i = 0; i < LineMax; i++)
        {
            for (int j = 0; j < LineMax; j++)
            {
                GameBoard[j, i] = StartBoard[j, i];
            }
        }
        judge.Lignt(GameBoard, whiteorblack, ref turn, ref endjudge);
        BoardUpdate();

        Material materialempty = Empty.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
        Material materialwhite = White.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
        Material materialblack = Black.GetComponentInChildren<SpriteRenderer>().sharedMaterial;

        Color color = materialempty.color;
        color.a = 1.0f;
        materialempty.color = color;

        Color colorwhite = materialwhite.color;
        color.a = 1.0f;
        materialwhite.color = color;

        Color colorblack = materialblack.color;
        color.a = 1.0f;
        materialblack.color = color;

    }


    public void OpenMenu()
    {
        if (menucheck)
        {
            retrybutton.gameObject.SetActive(true);
            titlebutton.gameObject.SetActive(true);

            Material materialempty = Empty.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
            Material materialwhite = White.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
            Material materialblack = Black.GetComponentInChildren<SpriteRenderer>().sharedMaterial;

            Color color = materialempty.color;
            color.a = 0.3f;
            materialempty.color = color;

            Color colorwhite = materialwhite.color;
            color.a = 0.3f;
            materialwhite.color = color;

            Color colorblack = materialblack.color;
            color.a = 0.3f;
            materialblack.color = color;
            menucheck = false;
        }

        else if (!menucheck)
        {
            retrybutton.gameObject.SetActive(false);
            titlebutton.gameObject.SetActive(false);

            Material materialempty = Empty.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
            Material materialwhite = White.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
            Material materialblack = Black.GetComponentInChildren<SpriteRenderer>().sharedMaterial;

            Color color = materialempty.color;
            color.a = 1.0f;
            materialempty.color = color;

            Color colorwhite = materialwhite.color;
            color.a = 1.0f;
            materialwhite.color = color;

            Color colorblack = materialblack.color;
            color.a = 1.0f;
            materialblack.color = color;
            menucheck = true;
        }
    }


}
