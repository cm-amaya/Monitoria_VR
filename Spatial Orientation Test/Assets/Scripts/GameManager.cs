using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public bool playersTurn = false;
    [HideInInspector] public bool  reset= false;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public DrawLine lineDrawer;
    private int countKey = 0;

    public bool draw = false;
    public Vector3 init;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        lineDrawer = GetComponent<DrawLine>();
        InitGame();
    }

    void InitGame()
    {
        playersTurn = false;
        reset = false;
        boardScript.BoardSetup();
    }

    private void Update()
    {

        if (playersTurn||draw)
            return;

        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (countKey == 0)
            {
                boardScript.BoardUpdate();
                countKey++;
            }
            else if (countKey == 1)
            {
                draw = false;
                boardScript.Visibility(true);
                countKey++;
            }
            else
            {
                boardScript.resetBoard();
                lineDrawer.Reset();
                reset = true;
                countKey = 0;
            }
        }
    }

}
