using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPointersScript : MonoBehaviour
{
    Transform _playerTransform;
    Tuple<Transform, Image, Transform>[] _pointers; // canvas, cavasImage, targetEnemy
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMainScript>().transform;
        Canvas[] tmp = GetComponentsInChildren<Canvas>();
        _pointers = new Tuple<Transform, Image, Transform>[tmp.Length];
        for (int i = 0; i < tmp.Length; i++) {
            _pointers[i] = new Tuple<Transform, Image, Transform>(tmp[i].transform, tmp[i].GetComponentInChildren<Image>(), null);
            tmp[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (Tuple<Transform, Image, Transform> element in _pointers) {
            if (element.Item3 != null) {
                Vector3 direction = (element.Item3.position - _playerTransform.position).normalized;

                float degress = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
                element.Item1.eulerAngles = new Vector3(90, degress, 0);
            }
        }
    }

    public void SetTarget(Transform target, Color color)
    {
        for (int i = 0; i < _pointers.Length; i++) {
            if (_pointers[i].Item3 == target) {
                _pointers[i].Item2.color = color;
                return;
            }
        }

        for (int i = 0; i < _pointers.Length; i++) {
            if (_pointers[i].Item3 == null) 
            {
                _pointers[i] = new Tuple<Transform, Image, Transform>(_pointers[i].Item1, _pointers[i].Item2, target);
                _pointers[i].Item2.color = color;
                _pointers[i].Item1.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void ClearTarget(Transform target) {
        for (int i = 0; i < _pointers.Length; i++)
        {
            if (_pointers[i].Item3 == target)
            {
                _pointers[i] = new Tuple<Transform, Image, Transform>(_pointers[i].Item1, _pointers[i].Item2, null);
                _pointers[i].Item1.gameObject.SetActive(false);
                break;
            }
        }
    }
}
