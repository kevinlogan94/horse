using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject HorseUIPanel;
    
    public void DisableActiveState()
    {
        gameObject.SetActive(false);
    }

    public void ActivateNewHorsePanel()
    {
        HorseUIPanel.SetActive(true);
    }
}
