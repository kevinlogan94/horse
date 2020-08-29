using System.Linq;
using TMPro;
using UnityEngine;

public class IncrementButton : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private AudioManager _audioManager;
    public static int ClickerLevel = 0;
    public static int ClickCount = 0;

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void Increment()
    {
        var randomNumber = Random.Range(0.0f, 3.0f);
        var increment = 1;
        if (randomNumber <= 0.03)
        {
            increment = ClickerLevel > 0 ? ClickerLevel * 300 : 100;
            Monitor.Instance.IncrementHorses(increment, "Unicorn");
        } 
        else if (randomNumber <= 0.30)
        {
            var helperHorse = ShopManager.Instance.Helpers[ClickerLevel + 1].HorseBreed;
            increment = ClickerLevel > 0 ? ClickerLevel * 45 : 3;
            Monitor.Instance.IncrementHorses(increment, helperHorse);
        }
        else
        {
            var helperHorse = ShopManager.Instance.Helpers[ClickerLevel].HorseBreed;
            increment = ClickerLevel > 0 ? ClickerLevel * 15 : increment;
            Monitor.Instance.IncrementHorses(increment, helperHorse);
        }
            
        _audioManager.Play("Cork", randomNumber);
        if (randomNumber <= 0.03)
        {
            var obj = _objectPooler.SpawnFromPool("IncrementBonusText");
            obj.GetComponentInChildren<TextMeshProUGUI>().text = "+" + increment;
        }
        else
        {
            var obj = _objectPooler.SpawnFromPool("IncrementText");
            obj.GetComponentInChildren<TextMeshProUGUI>().text = "+" + increment;
        }

        ClickCount++;
        Monitor.DestroyObject("FingerPointerIncrementButton");
    }
}
