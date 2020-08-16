using UnityEngine;

public class ShopBackButton : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject HelperScrollView;
    public GameObject UpgradeScrollView;
    public GameObject OptionsPanel;
    private AudioManager _audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void Back()
    {
        if (OptionsPanel.activeSelf)
        {
            ShopPanel.SetActive(false);
            _audioManager.Play("DoorBell");
        }
        else if (HelperScrollView.activeSelf)
        {
            HelperScrollView.SetActive(false);
            OptionsPanel.SetActive(true);
            _audioManager.Play("HighHat");
        } else if (UpgradeScrollView.activeSelf)
        {
            UpgradeScrollView.SetActive(false);
            OptionsPanel.SetActive(true);
            _audioManager.Play("HighHat");
        }
    }
}
