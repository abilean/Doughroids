using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject _mainMenu;

    [SerializeField]
    private ContinueBtn _continueButton;

    private void Awake()
    {
        GameManager.Instance.OnGamePausedChg += HandlePauseChg;
        GameManager.Instance.OnLevelChanged += HandleChangeLevel;
        GameManager.Instance.OnPlayerDied += HandlePlayerDied;
    }

    // Use this for initialization
    void Start () {
        if(_continueButton == null)
        {
            _continueButton = _mainMenu.transform.GetComponentInChildren<ContinueBtn>();
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandlePauseChg(bool paused)
    {
        if (paused)
            OpenPauseMenu();
    }

    private void HandleChangeLevel(int lvl)
    {
        if (lvl == 0)
            OpenMainMenu();
    }

    private void HandlePlayerDied()
    {
        //TODO: open game over menu
    }

    /// <summary>
    /// Opens main Menu with continue button
    /// </summary>
    private void OpenPauseMenu()
    {
        _mainMenu.SetActive(true);
        if(_continueButton != null)
        {
            _continueButton.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Opens the main menu, without continue button
    /// </summary>
    private void OpenMainMenu()
    {
        _mainMenu.SetActive(true);
        if(_continueButton != null)
        {
            _continueButton.gameObject.SetActive(false);
        }
    }

    public void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGamePausedChg -= HandlePauseChg;
            GameManager.Instance.OnLevelChanged -= HandleChangeLevel;
            GameManager.Instance.OnPlayerDied -= HandlePlayerDied;
        }

    }

}
