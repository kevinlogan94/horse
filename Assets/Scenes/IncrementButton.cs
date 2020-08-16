using System.Linq;
using TMPro;
using UnityEngine;

public class IncrementButton : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private AudioManager _audioManager;

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void Increment()
    {
        var clickerUpgrade = ShopManager.Instance.Upgrades.FirstOrDefault(x => x.name == "Clicker");
        if (clickerUpgrade == null)
        {
            Debug.LogWarning("We couldn't find the Clicker Upgrade on the ShopManager");
            return;
        }
        
        var randomNumber = Random.Range(0.0f, 3.0f);
        var increment = 1;
        if (randomNumber < 0.5)
        {
            var helperHorse = ShopManager.Instance.Helpers[clickerUpgrade.Level + 1].HorseBreed;
            increment = clickerUpgrade.Level > 0 ? clickerUpgrade.Level * 15 : 3;
            Monitor.Instance.IncrementHorses(increment, helperHorse);
        }
        else
        {
            var helperHorse = ShopManager.Instance.Helpers[clickerUpgrade.Level].HorseBreed;
            increment = clickerUpgrade.Level > 0 ? clickerUpgrade.Level * 10 : increment;
            Monitor.Instance.IncrementHorses(increment, helperHorse);
        }
            
        _audioManager.Play("Cork", randomNumber);
        var obj = _objectPooler.SpawnFromPool("IncrementText");
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "+" + increment;
        Monitor.DestroyObject("FingerPointerIncrementButton");
    }
}
