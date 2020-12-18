using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseTitleScreen()
    {
        gameObject.SetActive(false);
        switch (CanvasBackgroundController.Instance.CurrentCanvasBackground.ToString())
        {
            case "River":
                FindObjectOfType<AudioManager>().Play("River");
                break;
            case "Meadow":
                FindObjectOfType<AudioManager>().Play("Meadow");
                break;
            case "Altar":
                FindObjectOfType<AudioManager>().Play("Altar");
                break;
        }
    }
}
