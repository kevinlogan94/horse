using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class IncrementButton : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private AudioManager _audioManager;

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void Increment(int increment = 1)
    {
        var randomNumber = Random.Range(0.0f, 3.0f);
        _audioManager.Play("Cork", randomNumber);
        var obj = _objectPooler.SpawnFromPool("IncrementText");
        if (randomNumber<0.5)
        {
            increment*=3;
        }
        obj.GetComponent<TextMeshProUGUI>().text = "+" + increment;
        Monitor.IncrementHorses(increment);
        
        // StartCoroutine(RemoveAfterSeconds(1, obj));
    }

    //https://forum.unity.com/threads/hide-object-after-time.291287/
    // private static IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    // {
    //     yield return new WaitForSeconds(seconds);
    //     obj.SetActive(false);
    // }

}
