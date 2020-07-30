using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementButton : MonoBehaviour
{
    public void Increment(int increment = 1)
    {
        var randomNumber = Random.Range(0.0f, 3.0f);
        FindObjectOfType<AudioManager>().Play("Cork", randomNumber);
        if (randomNumber<0.5)
        {
            increment*=3;
        }
        Monitor.IncrementHorses(increment);
    }
}
