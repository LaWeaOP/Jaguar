using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { menu, inTheGame, pause, gameOver }
public enum TimeOfDay { Sunlight, Sunset, Night, Sunrise }

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager sharedInstance;
    public GameState currentStateGame = GameState.menu;
    public TimeOfDay currentTime = TimeOfDay.Sunlight;
    public Text scoreText;
    public Text dayTimer;
    public GameObject ice;
    public GameObject enemy;
    public float dayDuration;
    int dayTime = 1;
    int lootScore = 0;
    public float time;
    float clock = 12;
    #endregion

    void Awake()
    {
        dayDuration -= dayDuration % 6;
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
        clock += Time.deltaTime * (Time.deltaTime * 50) / (dayDuration / 24);
        dayTimer.text = (int)(clock % 24) + ":" + (int)(clock % 1 / 100 * 600) + "0";
        time = time + Time.deltaTime;
        if (time > dayDuration / 6)
        {
            time = time - dayDuration / 6;
            dayTime++;
            switch (dayTime%6)
            {
                case (1):
                    currentTime = TimeOfDay.Sunlight;
                    break;
                case (3):
                    currentTime = TimeOfDay.Sunset;
                    break;
                case (4):
                    currentTime = TimeOfDay.Night;
                    break;
                case (0):
                    currentTime = TimeOfDay.Sunrise;
                    break;
                default:
                    break;
            }
        }
    }

    public void AddPoint()
    {
        lootScore++;
        scoreText.text = "Score: " + lootScore.ToString();
    }
}
