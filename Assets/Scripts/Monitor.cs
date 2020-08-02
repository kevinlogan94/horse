﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class Monitor : MonoBehaviour
{

    public static int TotalHorsesEarned = 0;
    public static int Horses = 0;
    public static int PlayerLevel = 1;
    private ObjectPooler _objectPooler;
    private AudioManager _audioManager;

    #region Singleton
    public static Monitor Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void IncrementHorses(int increment = 1)
    {
        Horses += increment;
        TotalHorsesEarned += increment;
        TriggerHorse();
    }

    public void TriggerHorse()
    {
            _objectPooler.SpawnFromPool("Horse", new Vector3(0, Random.Range(350, 2000)));
    }
}
