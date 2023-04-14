using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointerScript : MonoBehaviour
{
    public Transform playerTransform;
    public Transform[] objective;
    public Transform canvas;
    int objectiveCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (objective[objectiveCounter].position - playerTransform.position).normalized;

        float degress = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
        canvas.eulerAngles = new Vector3(90, degress, 0);
    }

    public void ReachedObjective() {
        objectiveCounter++;
        print(objectiveCounter);
    }
}
