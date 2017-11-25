using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour {

    /// <summary>
    /// reference to this objects Text component
    /// </summary>
    private Text _scoreText;

    /// <summary>
    /// How many second the color stays changed
    /// </summary>
    private float _delay = 0.5f;

    private void Awake()
    {
        this._scoreText = this.transform.GetComponent<Text>();
        GameManager.Instance.OnScoreChanged += HandleScoreChange;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleScoreChange(int score)
    {
        int oldscore = int.Parse(_scoreText.text);
        if(oldscore < score)                            //Score went up
        {
           StartCoroutine( ChangeColor(Color.green));
        }else if(oldscore > score)                      //Score went down
        {
           StartCoroutine( ChangeColor(Color.red));
        }
        _scoreText.text = score.ToString();
    }


    private IEnumerator ChangeColor(Color nColor)
    {
        _scoreText.color = nColor;
        yield return new WaitForSeconds(_delay);
        _scoreText.color = Color.white;
    }
}
