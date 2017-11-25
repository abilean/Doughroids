using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivator : MonoBehaviour {

    /// <summary>
    /// The player
    /// </summary>
    [SerializeField]
    private GameObject _player;

    private void Awake()
    {
        if(_player == null)
            _player = this.transform.GetChild(0).gameObject;

        GameManager.Instance.OnLevelChanged += HandleChangeLevel;
    }

    // Use this for initialization
    void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleChangeLevel(int lvl)
    {
        if(lvl == 0)
        {
            _player.SetActive(false);
        }
        else
        {
            _player.transform.localPosition = Vector3.zero;
            _player.SetActive(true);

        }
    }

    public void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged -= HandleChangeLevel;
        }
    }
}
