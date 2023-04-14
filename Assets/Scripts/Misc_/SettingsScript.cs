using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Button _sprint, _crouch, _cameraMode, _jump, _interact;

    GameObject currentKey;
    // Start is called before the first frame update
    void Start()
    {
        _sprint.GetComponentInChildren<Text>().text = KeyBinding.keys["Sprint"].ToString();
        _crouch.GetComponentInChildren<Text>().text = KeyBinding.keys["Crouch"].ToString();
        _cameraMode.GetComponentInChildren<Text>().text = KeyBinding.keys["CameraMode"].ToString();
        _jump.GetComponentInChildren<Text>().text = KeyBinding.keys["Jump"].ToString();
        _interact.GetComponentInChildren<Text>().text = KeyBinding.keys["Interact"].ToString();
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
                KeyBinding.keys[currentKey.name] = e.keyCode;
                currentKey.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) {
        currentKey = clicked;
    }
}
