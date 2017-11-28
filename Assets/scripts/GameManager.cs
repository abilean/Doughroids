using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{

    /// <summary>
    /// The actual score, do not access variable directly, Use Score
    /// </summary>
    [SerializeField]
    private int _score = 0;

    /// <summary>
    /// indicates if the game is paused
    /// </summary>
    private bool _paused = false;

    /// <summary>
    /// The Width of the board, set for 960X600 resolution by default
    /// </summary>
    [SerializeField]
    private float _boardWidth = 51.11794f;
    /// <summary>
    /// The Height of the board, set for 960x600 resolution by default
    /// </summary>
    [SerializeField]
    private float _boardHeight = 28.2f;

    /// <summary>
    /// Which level the Player is on, 0 is Main Menu
    /// </summary>
    private int _level = 0;



    public int Score
    {
        get { return _score; }
        private set
        {
            _score = value;
            if (OnScoreChanged != null)
                OnScoreChanged(_score);
        }
    }

    /// <summary>
    /// The Width of the board, set for 960X600 resolution by default
    /// </summary>
    public float BoardWidth
    {
        get { return _boardWidth; }
    }

    /// <summary>
    /// The Height of the board, set for 960x600 resolution by default
    /// </summary>
    public float BoardHeight
    {
        get { return _boardHeight; }
    }

    public int Level
    {
        get { return _level; }
        private set
        {
            _level = value;
            Time.timeScale = 1;
            if (OnLevelChanged != null)
                OnLevelChanged(_level);
        }
    }

    /// <summary>
    /// Event thrown when score changed
    /// Int is the new score
    /// </summary>
    public Action<int> OnScoreChanged = delegate { };

    /// <summary>
    /// event thrown when Level changes 
    /// Int is the new Level
    /// </summary>
    public Action<int> OnLevelChanged = delegate { };

    /// <summary>
    /// event thrown when all enemies are dead
    /// </summary>
    public Action OnAllEnemiesDied = delegate { };

    /// <summary>
    /// Event thrown when the player dies
    /// </summary>
    public Action OnPlayerDied = delegate { };

    /// <summary>
    /// Event thrown when the game becomes paused
    /// bool = true when paused
    /// bool = false when unpaused
    /// </summary>
    public Action<bool> OnGamePausedChg = delegate { };

    /// <summary>
    /// The current player in the game
    /// GameObject = player gameobject
    /// </summary>
    public Action<GameObject> OnPlayerCreated = delegate { };


    protected GameManager()
    {

    }

    // Use this for initialization
    void Start () {
        Score = 0;
        Level = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreaseScore(int amount)
    {
        Score += amount;
    }

    public void DecreaseScore(int amount)
    {
        Score -= amount;
    }

    /// <summary>
    /// Resets Level to 0;
    /// </summary>
    public void PlayerDied()
    {
        //Time.timeScale = 0;

        if (OnPlayerDied != null)
            OnPlayerDied();
        StartCoroutine(ChangeLevel(0, 2f));

        
    }

    /// <summary>
    /// Increments Level by 1 
    /// </summary>
    public void AllEnemiesDied()
    {
        if (OnAllEnemiesDied != null)
            OnAllEnemiesDied();

        //Time.timeScale = 0;

        StartCoroutine(ChangeLevel(Level + 1, 2f));
    }

    /// <summary>
    /// Starts the game back at level 1, resets anything that need to be reset
    /// </summary>
    public void StartNewGame()
    {
        Score = 0;
        Level = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

        _paused = true;

        if (OnGamePausedChg != null)
            OnGamePausedChg(true);

    }

    /// <summary>
    /// Closes menu and puts game back into motion
    /// </summary>
    public void UnPauseGame()
    {

        Time.timeScale = 1;

        _paused = false;

        if (OnGamePausedChg != null)
            OnGamePausedChg(false);
    }


    public void CreatedNewPlayer(GameObject player)
    {
        if(player != null && OnPlayerCreated != null)
        {
            OnPlayerCreated(player);
        }
    }


    /// <summary>
    /// Changes the level
    /// </summary>
    /// <param name="lev">Level to be changed to</param>
    /// <param name="delay">The time to delay before executing</param>
    private IEnumerator ChangeLevel(int lev, float delay)
    {
        yield return new WaitForSeconds(delay);
        Level = lev;
    }

}
