using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Button sprint, crouch, cameraMode, jump, interact;

    GameObject _currentKey;
    // Start is called before the first frame update
    void Start()
    {
        sprint.GetComponentInChildren<Text>().text = KeyBinding.keys["Sprint"].ToString();
        crouch.GetComponentInChildren<Text>().text = KeyBinding.keys["Crouch"].ToString();
        cameraMode.GetComponentInChildren<Text>().text = KeyBinding.keys["CameraMode"].ToString();
        jump.GetComponentInChildren<Text>().text = KeyBinding.keys["Jump"].ToString();
        interact.GetComponentInChildren<Text>().text = KeyBinding.keys["Interact"].ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (_currentKey) {
            Event e = Event.current;
            if (e.isKey) {
                KeyBinding.keys[_currentKey.name] = e.keyCode;
                _currentKey.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                _currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) {
        _currentKey = clicked;
    }
}
