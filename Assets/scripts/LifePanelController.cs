using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanelController : MonoBehaviour {

    /// <summary>
    /// The images the represent player health
    /// </summary>
    private List<Image> _lifeMarkers;

    /// <summary>
    /// Health component for the player
    /// </summary>
    private IHealth _player;

    [SerializeField]
    private GameObject MarkerPrefab;


	// Use this for initialization
	void Start () {
		if(_lifeMarkers == null)
        {
            _lifeMarkers = new List<Image>();
        }



        GameManager.Instance.OnPlayerCreated += HandlePlayerCreated;


        SetBlank();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// When the player is created get the new Ihealth and set life counters
    /// </summary>
    /// <param name="player">The gameobject for the player</param>
    private void HandlePlayerCreated(GameObject player)
    {
        if (player != null)
        {
            _player = player.GetComponent<IHealth>();
            if (_player != null)
            {
                _player.OnHealthChg += SetLife;


                SetLife(_player.CurrentHp);
            }
        }
    }

    /// <summary>
    /// clears all the life markers, no health
    /// </summary>
    private void SetBlank()
    {
        foreach (Image img in _lifeMarkers)
        {
            img.enabled = false;
        }
    }


    /// <summary>
    /// sets the life markers to represent the players currentHP
    /// </summary>
    /// <param name="currentHp">players current health</param>
    private void SetLife(int currentHp)
    {
        int i = 0;
        for (; i < currentHp; i++)
        {
            //creates a marker if there arn't enough
            if(i >= _lifeMarkers.Count)
            {
                _lifeMarkers.Add(Instantiate(MarkerPrefab, this.transform).GetComponent<Image>());
            }

            _lifeMarkers[i].enabled = true;
        }

        //Makes sure all markers over health amount are disabled
        for(int x = i; x < _lifeMarkers.Count; x++)
        {
            _lifeMarkers[x].enabled = false;
        }
    }


    private void OnDestroy()
    {

    }
}
