using UnityEngine;

public class BuffPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectAndCloseSplash()
    {
        BuffManager.Instance.TriggerBuff(BuffType.Mana, 15);
        SplashManager.Instance.CloseSplash();
    }

    public void WatchAdAndCloseSplash()
    {
        AdvertisementManager.Instance.ShowBuffRewardAd(BuffType.Mana, 30);
        SplashManager.Instance.CloseSplash();
    }
}
