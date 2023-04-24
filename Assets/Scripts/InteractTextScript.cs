using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractTextScript : MonoBehaviour
{
    Text _interactPopupText;

    // Start is called before the first frame update
    void Start()
    {
        _interactPopupText = GetComponentInChildren<Text>();
        _interactPopupText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveText()
    {
        _interactPopupText.text = "";
    }
    public void SetText()
    {
        _interactPopupText.text = "Press " + KeyBinding.interact() + " to interact";
    }
}
