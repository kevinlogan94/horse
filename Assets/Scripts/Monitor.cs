using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Monitor : MonoBehaviour
{

    public static int TotalHorsesEarned = 0;
    public static int Horses = 0;
    public static int PlayerLevel = 1;
    
    // Update is called once per frame
    void Update()
    {
    }

    public static void IncrementHorses(int increment = 1)
    {
        Horses += increment;
        TotalHorsesEarned += increment;
    }
}
