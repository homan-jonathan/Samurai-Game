using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameCanvasScript gameCanvas;
    public GameObject interactCanvas;
    
    bool _isPaused = false;
    bool _gameOver = false;
    float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (PlayerPrefs.HasKey("PlayerMaterial"))
        {
            pauseMenu.GetComponentInChildren<SettingsScript>(true).FindAndSetPlayerMat();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameOver) {
            PauseGame();
        }

        if (!_gameOver) { 
            _timer += Time.deltaTime;
        }
    }

    public void PauseGame() {
        _isPaused = !_isPaused;
        pauseMenu.SetActive(_isPaused);

        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>()) {
            if (_isPaused) {
                audioSource.Pause(); 
            } else {
                audioSource.Play();
            }
        }

        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = _isPaused ? 0 : 1;
    }

    public float GetTimeElapsed() {
        return _timer;
    }

    public void HasWon() {
        gameCanvas.DisplayEndGameText("Mission Succssesful", Color.green);
        _gameOver = true;
        interactCanvas.SetActive(false);
        StartCoroutine(EndScreen(true));
    }

    public void HasLost()
    {
        gameCanvas.DisplayEndGameText("Mission Failed", Color.red);
        _gameOver = true;
        interactCanvas.SetActive(false);
        StartCoroutine(EndScreen(false));
    }

    public IEnumerator EndScreen(bool hasWon)
    {
        yield return new WaitForSeconds(5f);
        float bestTime = 0;
        if (PlayerPrefs.HasKey("Score")) {
            bestTime = PlayerPrefs.GetFloat("Score");
        }
        if (hasWon && _timer > bestTime) {
            bestTime = _timer;
        }
        PlayerPrefs.SetFloat("Score", bestTime);

        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(Scene.titleScene);
    }
}
