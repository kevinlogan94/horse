using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public GameObject BuffCountDown;
    public GameObject BuffCreature;
    public int CountDownSecondsRemaining;
    private float _currentWaitBeforeDecrement;

    public bool CountDownStarted;
    public bool BuffActive;

    public bool BuffTutorialCompleted;
    public bool BuffedThisLevel;

    #region Singleton
    public static BuffManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BuffActive)
        {
            RemoveBuffs();
            DecrementCountDown();
        }
        CheckAndSpawnBuffCreature();
    }
    
    public void TriggerBuff(BuffType buffType, int seconds)
    {
        // we only have 1 buff currently so...
        CountDownSecondsRemaining = seconds;
        ManaBar.Instance.InfiniteManaBuffActive = true;
        BuffActive = true;
        
        BuffCountDown.SetActive(true);
        _currentWaitBeforeDecrement = Time.time + 1f; // wait time of 1 second
    }

    public void SpawnBuffCreature()
    {
        BuffCreature.SetActive(true);
    }

    private void CheckAndSpawnBuffCreature()
    {
        if (IncrementPanel.ClickCount > 0 
            && IncrementPanel.ClickCount % 150 == 0 
            && !BuffActive)
        {
            BuffCreature.SetActive(true);
            BuffedThisLevel = true; // TODO consider removing this.
        }
    }

    private void DecrementCountDown()
    {
        if (Time.time > _currentWaitBeforeDecrement && CountDownStarted)
        {
            CountDownSecondsRemaining--;
            _currentWaitBeforeDecrement = Time.time + 1f;
        }
    }

    private void RemoveBuffs()
    {
        if (CountDownSecondsRemaining <= 0)
        {
            ManaBar.Instance.InfiniteManaBuffActive = false;
            BuffActive = false;
            CountDownStarted = false;
        }
    }
}

public enum BuffType
{
    Mana
}