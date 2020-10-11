using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewHorseScript : MonoBehaviour
{
    public HorseObject Horse;
    public Image Image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;

    private Animator _animator;
    
    #region Singleton
    public static NewHorseScript Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Title.text = Horse.Name;
        Description.text = Horse.Description;
        if (Image.IsActive())
        {
            if (_animator == null)
            {
                _animator = Image.GetComponent<Animator>();
            }
            _animator.SetInteger("HorseAnimationInt", Horse.HorseAnimationInt);
        }
    }
}
