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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayZoneText("The Forest Valley"));
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time: " + gameSceneManager.GetTimeElapsed().ToString("#.##");
    }
    public IEnumerator DisplayZoneText(string locationName)
    {
        zoneText.text = "Entering: " + locationName;
        yield return new WaitForSeconds(3f);
        zoneText.text = "";
    }

    public void DisplayEndGameText(string msg, Color color) {
        endGameObject.GetComponentInChildren<Text>().text = msg;
        endGameObject.GetComponentInChildren<Text>().color = color;
        endGameObject.SetActive(true);
    }
}
