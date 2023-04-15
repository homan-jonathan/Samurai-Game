using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject settingsScreen;
    public GameObject howToPlayScreen;
    public Text scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        GetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetHighScore() {
        if (PlayerPrefs.HasKey("Score")) {
            scoreCounter.text = "Fastest Time: " + PlayerPrefs.GetFloat("Score").ToString("#.##");
        }
    }

    public void OnPlayButtonClicked() {
        SceneManager.LoadScene(Scene.gameScene);
    }

    public void OnHowToPlayButtonClicked() {
        titleScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void OnSettingsButtonClicked() {
        titleScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        titleScreen.SetActive(true);
        settingsScreen.SetActive(false);
        howToPlayScreen.SetActive(false);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
