using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    GameObject _prefab;
    List<GameObject> _objectPool = new List<GameObject>();
    bool _canGrow;
    public ObjectPool(GameObject prefab, int size, bool canGrow)
    {
        _prefab = prefab;
        _canGrow = canGrow;
        for (int i = 0; i < size; i++)
        {
            GameObject temp = GameObject.Instantiate(_prefab);
            temp.SetActive(false);
            _objectPool.Add(temp);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < _objectPool.Count; i++)
        {
            if (!_objectPool[i].activeSelf)
            {
                return _objectPool[i];
            }
        }

        if (_canGrow)
        {
            GameObject temp = GameObject.Instantiate(_prefab);
            temp.SetActive(false);
            _objectPool.Add(temp);
            return temp;
        }

        return null;
    }
}
