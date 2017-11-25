using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameBtn : MonoBehaviour {

	public void HandleNewGameBtn()
    {
        GameManager.Instance.StartNewGame();
    }
}
