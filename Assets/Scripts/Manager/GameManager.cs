using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;
    public event Action<int,int> OnScoreLost;

    [SerializeField] private List<WaterFaceUP> waterFaceUPInstances;

    private float waitingToLevelTimer = 3;
    private float waitingToStartTimer = 1;
    private float countDownToStartTimer = 3;
    private float WaitingToGameOver= 1.5f;
    private float gamePlayingTimer = 100f;
    private float score;
    private int totalLostScore = 0;
    public float requireScore = 5;
    private float gamePlayingTimeTotal;


    private string GameScene1 = "2_GameScene1";
    private string GameScene2 = "3_GameScene2";


    private bool isGamePause = false;

    private enum State
    {
        WaitingToStart,
        LevelShowing,
        CountDownToStart,
        GamePlaying,
        GameLosePoint,
        GameCongratuate,
        GameOver
    }
    private State state;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        
        gamePlayingTimeTotal = gamePlayingTimer;
    }
    void Start()
    {
        TurnToWaitingToStart();
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGame();

    }

    // Update is called once per frame
    void Update()
    {
        float score = ScoreManager.Instance.GetCurrentScore();
        string currentScene = SceneManager.GetActiveScene().name;
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    TurnToLevelShowing();
                    //TurnToCountDownToStart();
                }
                break;

            case State.LevelShowing:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    TurnToCountDownToStart();
                }
                break;


            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0)
                {
                    TurnToGamePlaying();
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    TurnToGameLosePoint();
                }
                break;

            case State.GameLosePoint:
                WaitingToGameOver-= Time.deltaTime;

                if (WaitingToGameOver <= 0)
                {
                    if (score >= requireScore && currentScene == GameScene1)
                    {
                        
                        TurnToGameCongratuate();
                        
                    }
                    else
                    {
                        TurnToGameOver();
                    }
                }

                break;

            case State.GameCongratuate:
                GameInput.Instance.OnInteractAction += ChangeToScene2_OnInteractAction;
                break;
            case State.GameOver:
                GameInput.Instance.OnInteractAction += ChangeToScene1_OnInteractAction1;
                break;
            default:
                break;
        }

    }

    private void ChangeToScene1_OnInteractAction1(object sender, EventArgs e)
    {
        ScoreManager.Instance.ResetScore();
        string currentScene = SceneManager.GetActiveScene().name;
       
        if (currentScene == GameScene1)
        {
            Loader.Load(Loader.Scene.GameScene1);
        }
        else if (currentScene == GameScene2)
        {
            Loader.Load(Loader.Scene.GameMenuScene);
        }

    }

    private void ChangeToScene2_OnInteractAction(object sender, EventArgs e)
    {

        string currentScene = SceneManager.GetActiveScene().name;

        Dictionary<int, int> lostScores = new Dictionary<int, int>();

        if (currentScene == GameScene1)
        {
            Loader.Load(Loader.Scene.GameScene2);
        }
        else if (currentScene == GameScene2)
        {
            Loader.Load(Loader.Scene.GameMenuScene);
        }

    }

    private void AdjustScoreBasedOnWaterLevel()
    {
        if (waterFaceUPInstances.Count == 0)
        {
            print("waterFaceUPInstances.Count = 0");
            return;
        }

        // 创建一个字典来存储每个水池的扣分
        Dictionary<int, int> lostScores = new Dictionary<int, int>();

        foreach (var waterFaceUP in waterFaceUPInstances)
        {
            float waterLevel = waterFaceUP.GetTFactor();
            int counterID = waterFaceUP.counterID;

            if (!lostScores.ContainsKey(counterID))
            {
                lostScores[counterID] = 0;
            }

            if (waterLevel > 0 && waterLevel <= 0.2f)
            {
                lostScores[counterID] += 3;
                ScoreManager.Instance.SubtractScore(3);
            }
            else if (waterLevel > 0.2f && waterLevel <= 0.5f)
            {
                lostScores[counterID] += 5;
                ScoreManager.Instance.SubtractScore(5);
            }
            else if (waterLevel > 0.5f && waterLevel <= 1f)
            {
                lostScores[counterID] += 10;
                ScoreManager.Instance.SubtractScore(10);
            }

            waterFaceUP.Reset();
        }

        // 通知每个水池的扣分
        foreach (var kvp in lostScores)
        {
            if (kvp.Value > 0)
            {
                NotifyScoreLoss(kvp.Key, kvp.Value);  // 传递counterID和lostScore
            }
        }

    }

    private void TurnToWaitingToStart()
    {
        state = State.WaitingToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToLevelShowing()
    {
        state = State.LevelShowing;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TurnToGameLosePoint()
    {
        AdjustScoreBasedOnWaterLevel();

        state = State.GameLosePoint;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TurnToGameOver()
    {
        //AdjustScoreBasedOnWaterLevel();
        state = State.GameOver;
        SoundManager.Instance.volume = 0;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TurnToGameCongratuate()
    {
        //AdjustScoreBasedOnWaterLevel();
        state = State.GameCongratuate;
        SoundManager.Instance.volume = 0;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }



    private void DisablePlayer()
    {
       player.enabled = false;
    }
    private void EnablePlayer()
    {
       player.enabled = true;
    }

    public bool IsLevelShowingState()
    {
        return state == State.LevelShowing;
    }

    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }

    public bool IsGamePlayingState()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameLosePointState()
    {
        return state == State.GameLosePoint;
    }

    public bool IsGameOverState()
    {
        return state == State.GameOver;
    }
    public bool IsGameCongratuateState()
    {
        return state == State.GameCongratuate;
    }

    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }

    public void ToggleGame()
    {
        isGamePause = !isGamePause;
        if(isGamePause)
        {
            Time.timeScale = 0;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnPause?.Invoke(this, EventArgs.Empty);
        }
    }


    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimeTotal;
    }

    public void NotifyScoreLoss(int counterID, int lostScore)
    {
        OnScoreLost?.Invoke(counterID, lostScore);
    }




}


