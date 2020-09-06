using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class NewHorseScript : MonoBehaviour
{
    public HorseObject Horse;
    public Image Image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    
    #region Singleton
    public static NewHorseScript Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Update is called once per frame
    void Update()
    {
        Title.text = Horse.Name;
        Description.text = Horse.Description;
        if (Image.IsActive())
        {
            var animator = Image.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetInteger("HorseAnimationInt", Horse.HorseAnimationInt);
            }   
        }
    }
}
