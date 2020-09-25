using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsText : MonoBehaviour
{
    private float _waitTime = 5.0f;
    private bool _justTurnedOn = true;
    
    public void Start()
    {
        _waitTime = Time.time + _waitTime;
    }

    public void Update()
    {
        WaitThenDisableObject();
    }

    public void WaitThenDisableObject()
    {
        if (_justTurnedOn)
        {    
            Debug.Log(_waitTime);
            _waitTime = Time.time + _waitTime;
            Debug.Log(_waitTime);
            _justTurnedOn = false;
        }
        if (Time.time > _waitTime)
        {
            _justTurnedOn = true;
            gameObject.SetActive(false);
        }
    }
}
