using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointerScript : MonoBehaviour
{
    Transform _playerTransform;
    public Transform[] objective;
    Transform _canvas;
    int _objectiveCounter = 0;
    Vector3 _lastDirection = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMainScript>().transform;
        _canvas = GetComponentInParent<Canvas>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (objective[_objectiveCounter].position - _playerTransform.position).normalized;

        if (Vector3.Angle(_lastDirection, direction) > 180 / 2)
        {
            _lastDirection = direction;
        }

        float degress = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
        _canvas.eulerAngles = new Vector3(90, degress, 0);
    }

    public void ReachedObjective() {
        _objectiveCounter++;
    }
}
