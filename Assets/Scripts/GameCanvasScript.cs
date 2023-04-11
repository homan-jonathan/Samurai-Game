using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasScript : MonoBehaviour
{
    public Text zoneText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayZoneText("The Forest Valley"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator DisplayZoneText(string locationName)
    {
        zoneText.text = "Entering: " + locationName;
        yield return new WaitForSeconds(3f);
        zoneText.text = "";
    }
}
