using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [Header("Level Settings")]
    [SerializeField] string levelAfterWin;
    [SerializeField] Text UILevelName;
    [SerializeField] Text UIScore;
    [SerializeField] Text UIScoreMultiplier;
    [SerializeField] GameObject[] UILife;

    [Header("Player Settings")]
    [SerializeField] int numStartLives = 4;
    [SerializeField] int numLives = 4;

    [Header("Score Settings")]
    [SerializeField] int Score = 0;
    [SerializeField, Tooltip("How long is the score multiplier active before resetting to 1")]
    float scoreMultiplierCountdown = 1f;

    [Header("Game Screens")]
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 cursorHotspot;
    [SerializeField] CursorMode cursorMode = CursorMode.Auto;

    [SerializeField] GameObject comboDisplay;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] Text loseScreenText;

    //============= Private vars =============
    [HideInInspector] public int scoreMultiplier = 0;
    [HideInInspector] public bool gameIsOver = false;
    GameObject _player;
    float _comboTimespan;   // Time span within which score multiplier accumulates


    private void Awake()
    {
        gm = GetComponent<GameManager>();
        SetupDefaults();
        Cursor.SetCursor(cursorTexture,cursorHotspot,cursorMode);
    }

    void Update () 
	{
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pauseScreen.activeInHierarchy)
                pauseScreen.SetActive(true);
            else
                pauseScreen.SetActive(false);
        }

        // reset score multiplier if time span passed with no new kills/scores
        if (Time.time > _comboTimespan)
        {
            scoreMultiplier = 0;
            _comboTimespan = 0;
            comboDisplay.SetActive(false);
        }
    }

    void SetupDefaults()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        RefreshGUI();
        comboDisplay.SetActive(false);
        pauseScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
 
    void RefreshGUI ()
    {
        UILevelName.text = SceneManager.GetActiveScene().name;
        UIScore.text = "Score: " + Score.ToString();
        UIScoreMultiplier.text = scoreMultiplier.ToString();

        for (int i = 0; i < UILife.Length ; i++ )
        {
            if (i < (numLives - 1))
                UILife[i].SetActive(true);
            else
                UILife[i].SetActive(false);
        }
    }

    public void GainLife (int lifeAmount)
    {
        if (numLives < numStartLives)
            numLives += lifeAmount;
        RefreshGUI();
    }

    public void LoseLife (int damageAmount)
    {
        numLives -= damageAmount;
        RefreshGUI();

        if (numLives <= 0)
            LoseGame();
    }

    public void AddScore (int scoreValue)
    {
        scoreMultiplier ++; // score multiplier starts from 0
        Score += scoreValue * scoreMultiplier;

        // update available time span where additional kills will add up score multiplier
        _comboTimespan = Time.time + scoreMultiplierCountdown;

        RefreshGUI();

        if (scoreMultiplier > 1)
            DisplayComboScreen();
    }

    void DisplayComboScreen()
    {
        comboDisplay.SetActive(true);
    }

    void LoseGame ()
    {
        _player.GetComponent<CharacterController2D>().DestroyPlayer();
        gameIsOver = true;
        loseScreen.SetActive(true);
        loseScreenText.text = string.Format("You Scored {0} points \n Keep going!!", Score);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(levelAfterWin);
    }
}
