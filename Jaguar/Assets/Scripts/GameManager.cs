using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    menu,
    inTheGame,
    pause,
    gameOver
}

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager sharedInstance;
    public GameState currentStateGame = GameState.menu;
    public Text scoreText;
    public GameObject ice;
    public GameObject enemy;
    public float dayDuration;
    int daysAlive;
    int lootScore = 0;
    float time;
    bool day;
    #endregion

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > dayDuration / 2)
        {
            if (day)
            {
                day = false;
            }
            else
            {
                day = true;
            }
        }
        if (time >= dayDuration)
        {
            time = 0;
            daysAlive++;
        }
    }

    public void AddPoint()
    {
        lootScore++;
        scoreText.text = "Score: " + lootScore.ToString();
    }
}
