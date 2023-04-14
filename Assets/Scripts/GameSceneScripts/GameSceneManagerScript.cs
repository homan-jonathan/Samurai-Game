using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerScript : MonoBehaviour
{
    public GameObject _pauseMenu;
    public GameCanvasScript _GameCanvas;
    public bool isPaused = false;
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

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void HasWon() {
        StartCoroutine(_GameCanvas.DisplayEndGameText("Mission Succssesful", Color.green));
    }

    public void HasLost()
    {
        StartCoroutine(_GameCanvas.DisplayEndGameText("Mission Failed", Color.red));
    }

    public void EndScreen() {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(Scene.titleScene);
    }
}
