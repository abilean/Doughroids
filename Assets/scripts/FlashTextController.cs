using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashTextController : MonoBehaviour {

    private Text _myText;

    [SerializeField]
    private float _TextVisibleTime = 2f;

	// Use this for initialization
	void Awake () {
        _myText = this.transform.GetComponent<Text>();

        GameManager.Instance.OnAllEnemiesDied += HandleLevelClear;
        GameManager.Instance.OnPlayerDied += HandleGameOver;
        GameManager.Instance.OnLevelChanged += HandleNewLevel;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleLevelClear()
    {
        StartCoroutine(DisplayText("Level Clear!"));
    }

    private void HandleGameOver()
    {
        StartCoroutine(DisplayText("Game Over!!!"));
    }

    private void HandleNewLevel(int lvl)
    {
        if(lvl != 0)
            StartCoroutine(DisplayText("Level " + lvl));
    }

    private IEnumerator DisplayText(string text)
    {
        _myText.text = text;
        _myText.enabled = true;

        yield return new WaitForSeconds(_TextVisibleTime);

        _myText.enabled = false;
    }
}
