using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Button _sprint, _crouch, _cameraMode, _jump;

    Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    GameObject currentKey;
    // Start is called before the first frame update
    void Start()
    {
        /*_sprint.GetComponentInChildren<Text>().text = KeyBinding.sprint.ToString();*/
        keys.Add("Sprint", KeyBinding.sprint);
        keys.Add("Crouch", KeyBinding.crouch);
        keys.Add("CameraMode", KeyBinding.cameraMode);
        keys.Add("Jump", KeyBinding.jump);
        _sprint.GetComponentInChildren<Text>().text = keys["Sprint"].ToString();
        _crouch.GetComponentInChildren<Text>().text = keys["Crouch"].ToString();
        _cameraMode.GetComponentInChildren<Text>().text = keys["CameraMode"].ToString();
        _jump.GetComponentInChildren<Text>().text = keys["Jump"].ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (currentKey) {
            Event e = Event.current;
            if (e.isKey) {
                //Back end mapping
                switch (currentKey.name)
                {
                    case "Sprint":
                        KeyBinding.sprint = e.keyCode;
                        break;
                    case "Crouch":
                        KeyBinding.crouch = e.keyCode;
                        break;
                    case "CameraMode":
                        KeyBinding.cameraMode = e.keyCode; 
                        break;
                    case "Jump":
                        KeyBinding.jump = e.keyCode;
                        break;
                }


                //Front end display stuff
                keys[currentKey.name] = e.keyCode;
                currentKey.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) {
        currentKey = clicked;
    }
}
