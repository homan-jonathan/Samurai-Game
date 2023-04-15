using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerScript : MonoBehaviour
{
    public GameObject _pauseMenu;
    public GameCanvasScript _GameCanvas;
    public bool isPaused = false;
    bool hasWon = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        isPaused = !isPaused;
        _pauseMenu.SetActive(isPaused);

        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>()) {
            if (isPaused) {
                audioSource.Pause(); 
            } else {
                audioSource.Play();
            }
        }

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void HasWon() {
        StartCoroutine(_GameCanvas.DisplayEndGameText("Mission Succssesful", Color.green));
        _GameCanvas.gameOver = true;
        hasWon = true;
    }

    public void HasLost()
    {
        StartCoroutine(_GameCanvas.DisplayEndGameText("Mission Failed", Color.red));
        _GameCanvas.gameOver = true;
    }

    public void EndScreen() {
        float bestTime = 0;
        if (PlayerPrefs.HasKey("Score")) {
            bestTime = PlayerPrefs.GetFloat("Score");
        }
        if (hasWon && _GameCanvas.timer > bestTime) {
            bestTime = _GameCanvas.timer;
        }
        PlayerPrefs.SetFloat("Score", bestTime);

        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(Scene.titleScene);
    }
}
