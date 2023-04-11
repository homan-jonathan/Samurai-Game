using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameSceneManagerScript _gameManager;
    public GameObject _pausedScreen;
    public GameObject _settingsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        ShowDefaultScreen();
    }

    public void OnUnpauseClicked()
    {
        _gameManager.PauseGame();
    }

    public void OnSettingsClicked()
    {
        _pausedScreen.SetActive(false);
        _settingsScreen.SetActive(true);
    }

    public void OnHomeClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Scene.titleScene);
    }

    public void OnBackButtonClicked() {
        ShowDefaultScreen();
    }

    void ShowDefaultScreen() {
        _settingsScreen.SetActive(false);
        _pausedScreen.SetActive(true);
    }
}
