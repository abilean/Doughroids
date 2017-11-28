using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    private enum State { Playing, Waiting };

    private State _currentState = State.Waiting;

    /// <summary>
    /// Multiplies this number by the level to find out number of Enemies (cuts off remaining decimals)
    /// </summary>
    [SerializeField]
    [Tooltip("Multiplies this number by the level to find out number of Enemies (cuts off remaining decimals)")]
    private float _enemyNumberMultiplier = 2f;

    /// <summary>
    /// The prefab for the enemy to spawn
    /// </summary>
    [SerializeField]
    private DonutController _enemyPrefab;


    /// <summary>
    /// pool for generating prefabs
    /// </summary>
    private Pool _pool;

    private int _donutCount = 0;

    private void Awake()
    {
        if (_enemyPrefab != null)
        {
            GameManager.Instance.OnLevelChanged += HandleChangeLevel;

            _pool = Pool.GetPool(_enemyPrefab);
        }
        else
        {
            Debug.LogError("No prefab was given for the EnemyGenerator!!!!");
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(_currentState == State.Playing)
        {
            if(_donutCount == 0)
            {
                GameManager.Instance.AllEnemiesDied();
                _currentState = State.Waiting;
            }
        }

        //Debug.Log("Enemies has " + this.transform.childCount + " children");
	}

    private void HandleChangeLevel(int level)
    {
        //disable all current enemies
        DisableEnemies();

        _donutCount = 0;

        //check for menu screen
        if(level == 0)
        {
            MakeEnemies(8);
            _currentState = State.Waiting;
            return;
        }

        //make the enemies for the current level
        MakeEnemies(Mathf.FloorToInt(level * _enemyNumberMultiplier));
        _currentState = State.Playing;
    }

    /// <summary>
    /// Disables all the current children of the enemies class
    /// </summary>
    private void DisableEnemies()
    {
        Transform temp;
        for(int i = 0; i < transform.childCount; i ++)
        {
            temp = this.transform.GetChild(i);
            if (temp != null)
                temp.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Makes a number of enemies to be children of the enemiesGenerator class
    /// they start with a random position
    /// </summary>
    /// <param name="num">Number of enemies to create</param>
    private void MakeEnemies(int num)
    {
        Vector3 pos;
        DonutController tempDonut;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        for(int i = 0; i < num; i++)
        {
            pos = new Vector3(Random.Range(0, GameManager.Instance.BoardWidth),
            3.5f,
            Random.Range(0, GameManager.Instance.BoardHeight));

            //Check that position is not close to player
            if (player != null)
            {
                if (IsNearPlayer(pos, player.transform.position))
                {
                    pos.x += 5;
                    pos.z += 5;
                }
            }

            //create a donut
            tempDonut = _pool.Get(pos, Quaternion.identity, this.transform) as DonutController;

            //make sure we know when the donut dies
            tempDonut.GetComponent<IHealth>().OnDie += HandleDonutDeath;

            //increment the donut since one has been added
            _donutCount++;
        }
    }

    public void OnDestroy()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.OnLevelChanged -= HandleChangeLevel;
    }

    /// <summary>
    /// Checks if the pos is close to the player 
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private bool IsNearPlayer(Vector3 pos, Vector3 playerPos)
    {


        if (Mathf.Abs(pos.x - playerPos.x) < 5)
            return true;

        if (Mathf.Abs(pos.z - playerPos.z) < 5)
            return true;

        return false;

    }

    private void HandleDonutDeath()
    {
        _donutCount--;
    }
}
