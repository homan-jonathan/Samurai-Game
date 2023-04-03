using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerScript : MonoBehaviour
{
    public GameObject _pauseMenu;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {

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

        float timeScale = isPaused ? 0 : 1;
        Time.timeScale = timeScale;
    }
}
