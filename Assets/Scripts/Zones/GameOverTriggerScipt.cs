using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTriggerScipt : MonoBehaviour
{
    public PlayerMainScript playerMainScript;
    public GameSceneManagerScript gameSceneManagerScript;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player) && playerMainScript.HasCoins())
        {
            gameSceneManagerScript.HasWon();
        }
    }
}
