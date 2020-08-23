using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ShopTabs : MonoBehaviour
{
    public Button HelperButton;
    public Button UpgradeButton;
    public GameObject UpgradeScrollView;
    public GameObject HelperScrollView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Click(string buttonName)
    {
        if (buttonName == "Helper")
        {
            SetTabbedDisplay(HelperButton);
            SetNonTabbedDisplay(UpgradeButton);
            HelperScrollView.SetActive(true);
            UpgradeScrollView.SetActive(false);
        } else if (buttonName == "Upgrade")
        {
            SetTabbedDisplay(UpgradeButton);
            SetNonTabbedDisplay(HelperButton);
            HelperScrollView.SetActive(false);
            UpgradeScrollView.SetActive(true);
        }
    }

    private void SetTabbedDisplay(Button button)
    {
        var buttonRect = button.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(buttonRect.sizeDelta.x,275);
        // button.transform.position = new Vector3(button.transform.position.x, -48);
    }

    private void SetNonTabbedDisplay(Button button)
    {
        var buttonRect = button.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(buttonRect.sizeDelta.x,200);
        // button.transform.position = new Vector3(button.transform.position.x, -30);
    }
}
