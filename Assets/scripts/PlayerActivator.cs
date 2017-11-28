using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivator : MonoBehaviour {

    /// <summary>
    /// The player
    /// </summary>
    [SerializeField]
    private GameObject _playerPrefab;



    private void Awake()
    {

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
            DestroyAllChildren();
        }
        else if(lvl == 1)
        {
            DestroyAllChildren();
            GameObject player = Instantiate(_playerPrefab, this.transform.position, this.transform.rotation, this.transform);
            GameManager.Instance.CreatedNewPlayer(player);
        }
    }


    private void DestroyAllChildren()
    {
        foreach(Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
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
