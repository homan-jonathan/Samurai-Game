using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameSceneManagerScript gameManager;
    public GameObject pausedScreen;
    public GameObject settingsScreen;
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
        gameManager.PauseGame();
    }

    public void OnSettingsClicked()
    {
        pausedScreen.SetActive(false);
        settingsScreen.SetActive(true);
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
        settingsScreen.SetActive(false);
        pausedScreen.SetActive(true);
    }
}
