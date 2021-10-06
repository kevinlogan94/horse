using UnityEngine;

public class PortalPanel : MonoBehaviour
{
    public GameObject MeadowButton;
    public GameObject RiverButton;
    public GameObject MountainButton;
    
    private AudioManager _audioManager;
    
    private Sprite _disableTeleportButtonImage;
    private Sprite _activeTeleportButtonImage;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _disableTeleportButtonImage = Resources.Load<Sprite>("achiev_box_pressed");
        _activeTeleportButtonImage = Resources.Load<Sprite>("achiev_box");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformTeleport(CanvasBackground background)
    {
        CanvasBackgroundController.Instance.UpdateCanvasBackground(background);
        ManaBar.Instance.DeductAllMana();
        _audioManager.Play("CoinToss");
    }
    
}