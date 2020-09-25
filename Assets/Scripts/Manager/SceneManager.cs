using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject TextBox;

    public string[] Banter;
    private int _banterIndex = 0;
    private float _banterWaitTime = 10f;
    private float _currentBanterWaitTime = 10f;
    private bool _banterActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisableBanterAfterNoInteraction();
    }

    public void TriggerBanter()
    {
        _currentBanterWaitTime = Time.time + _banterWaitTime;
        _banterActive = true;
        TextBox.SetActive(true);
        var textMeshPro = TextBox.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = Banter[_banterIndex];

        if (_banterIndex < Banter.Length - 1)
        {
            _banterIndex++;   
        }
        else
        {
            _banterIndex = 0;
        }
    }

    private void DisableBanterAfterNoInteraction()
    {
        if (_banterActive && TextBox.activeSelf && Time.time > _currentBanterWaitTime)
        {
            _banterActive = false;
            TextBox.SetActive(false);
        }
    }
}
