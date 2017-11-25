using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverOnDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {

        this.gameObject.GetComponent<IHealth>().OnDie += HandleDeath;
    }

    /// <summary>
    /// Tells the game manager that it's game over
    /// </summary>
    private void HandleDeath()
    {
        GameManager.Instance.PlayerDied();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
