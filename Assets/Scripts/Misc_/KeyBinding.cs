using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinding
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode> { { "Crouch", KeyCode.LeftControl }, { "Sprint", KeyCode.LeftShift }, { "Jump", KeyCode.Space }, { "CameraMode", KeyCode.C } };
    public static KeyCode crouch() { return keys["Crouch"]; }
    public static KeyCode sprint() { return keys["Sprint"]; }
    public static KeyCode jump() { return keys["Jump"]; }
    public static KeyCode cameraMode() { return keys["CameraMode"]; }
}
