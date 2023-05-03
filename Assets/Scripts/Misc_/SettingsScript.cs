using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Button sprint, crouch, cameraMode, jump, interact;
    public Material[] _playerMats;

    GameObject _currentKey;
    void Awake()
    {
        if (PlayerPrefs.HasKey("Sprint"))
        {
            KeyBinding.keys["Sprint"] = (KeyCode) Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint"));
            print(KeyBinding.keys);
        }
        if (PlayerPrefs.HasKey("Crouch"))
        {
            KeyBinding.keys["Crouch"] = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch"));
        }
        if (PlayerPrefs.HasKey("CameraMode"))
        {
            KeyBinding.keys["CameraMode"] = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CameraMode"));
        }
        if (PlayerPrefs.HasKey("Jump"))
        {
            KeyBinding.keys["Jump"] = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump"));
        }
        if (PlayerPrefs.HasKey("Interact"))
        {
            KeyBinding.keys["Interact"] = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact"));
        }
    }

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
                PlayerPrefs.SetString(_currentKey.name, e.keyCode.ToString());
                _currentKey.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                _currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) {
        _currentKey = clicked;
    }

    public void MaterialClicked(int matIndex)
    {
        PlayerPrefs.SetInt("PlayerMaterial", matIndex);
        FindAndSetPlayerMat();
    }

    public void FindAndSetPlayerMat() {
        PlayerMainScript player = FindObjectOfType<PlayerMainScript>();
        if (player != null)
        {
            player.GetComponentInChildren<SkinnedMeshRenderer>().material = _playerMats[PlayerPrefs.GetInt("PlayerMaterial")];
        }
    }
}
