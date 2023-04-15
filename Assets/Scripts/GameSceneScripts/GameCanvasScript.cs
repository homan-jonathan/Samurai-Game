using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasScript : MonoBehaviour
{
    public GameSceneManagerScript gameSceneManager;
    public Text timerText;
    public Text zoneText;
    public GameObject endGameObject;

    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayZoneText("The Forest Valley"));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "Time: " + timer.ToString("#.##");
    }
    public IEnumerator DisplayZoneText(string locationName)
    {
        zoneText.text = "Entering: " + locationName;
        yield return new WaitForSeconds(3f);
        zoneText.text = "";
    }

    public IEnumerator DisplayEndGameText(string msg, Color color) {
        endGameObject.GetComponentInChildren<Text>().text = msg;
        endGameObject.GetComponentInChildren<Text>().color = color;
        endGameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        gameSceneManager.EndScreen();
    }
}
