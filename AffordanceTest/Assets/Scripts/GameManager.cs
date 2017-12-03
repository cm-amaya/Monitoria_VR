using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public bool nextLevel;
    public bool canIPass;
    private float timer;
    private int currentLevel;

    private string filePath;
    private string[] scores;
    private bool finishGame;

    public BoardManager boardScript;

    private void Awake(){
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        scores = new string[19];
        scores[0] = "Level Distance_to_Poles Distance_between_poles Time Can_I_Pass?";
        currentLevel = 1;
        timer = 0;
        finishGame = false;
        nextLevel = false;
        boardScript.BoardSetup();
    }


	// Update is called once per frame
	void Update (){
        if (finishGame) return;
        if (nextLevel)
        {
            scores[currentLevel] = currentLevel+ " "+boardScript.orderDistances[currentLevel - 1] + " " + boardScript.orderPoles[currentLevel - 1] + " "+timer.ToString()+" "+canIPass.ToString();
            Debug.Log(scores[currentLevel]);
            timer = 0;
            currentLevel +=1;
            if (currentLevel >= scores.Length)
            {
                finishGame = true;
                StartCoroutine(finish());
            }
            else
            {
                boardScript.BoardUpdate(currentLevel);
                Debug.Log("Next Level");
            }
            nextLevel = false;
        }
        else
        {
            timer += Time.deltaTime;
        }    
	}

    IEnumerator finish()
    {
        scoreFile();
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    //This class stores information for each of the levels.
    void  scoreFile()
    {
        filePath = Application.persistentDataPath + "/"+String.Concat(System.DateTime.UtcNow.ToString("HHmm_ddMMMMyyyy"),".txt");
        Debug.Log(filePath);
        try
        {
            if (!File.Exists(filePath))
            {
                Debug.Log("Opened file!");
                Debug.Log("About to write into file!");
                File.WriteAllLines(filePath, scores);
            }
            else
            {
                Debug.Log("File is exist!");
            }
        }

        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
