using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNighCycleScript : MonoBehaviour
{
    public float tickLength = .5f;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > tickLength)
        {
            transform.Rotate(.5f * tickLength, 0, 0);
            timer = 0;
        }
    }
}
