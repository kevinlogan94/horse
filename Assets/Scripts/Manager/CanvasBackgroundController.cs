using UnityEngine;
using UnityEngine.UI;

public class CanvasBackgroundController : MonoBehaviour
{
    private Sprite _meadowImage;
    private Sprite _riverImage;
    private Sprite _altarImage;

    public CanvasBackground CurrentCanvasBackground;

    public static CanvasBackgroundController Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
        _meadowImage = Resources.Load<Sprite>("Backgrounds/Horizon");
        _riverImage = Resources.Load<Sprite>("Backgrounds/River");
        _altarImage = Resources.Load<Sprite>("Backgrounds/Altar");
    }

    void Update()
    {
        UpdateCanvasBackground(CurrentCanvasBackground);
    }

    public void UpdateCanvasBackground(CanvasBackground background)
    {
        Sprite spriteToUse;
        switch (background.ToString())
        {
            case "River":
                spriteToUse = _riverImage;
                break;
            case "Meadow":
                spriteToUse = _meadowImage;
                break;
            case "Altar":
                spriteToUse = _altarImage;
                break;
            default:
                spriteToUse = _meadowImage;
                break;
        }
        gameObject.GetComponent<Image>().sprite = spriteToUse;
        CurrentCanvasBackground = background;
    }
}

public enum CanvasBackground
{
    River,
    Meadow,
    Altar
}
