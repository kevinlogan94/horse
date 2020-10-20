using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementText : MonoBehaviour
{
    public GameObject ParentPanel;
    
    public void DisableActiveState()
    {
        ParentPanel.SetActive(false);
    }
}
