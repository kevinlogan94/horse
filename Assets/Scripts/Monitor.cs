using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monitor : MonoBehaviour
{

    public static int TotalHorsesEarned = 0;
    public static int Horses = 0;
    public static int PlayerLevel = 1;
    private ObjectPooler _objectPooler;

    #region Singleton
    public static Monitor Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void IncrementHorses(int increment, string horseBreed, float lagSeconds = 0)
    {
        Horses += increment;
        TotalHorsesEarned += increment;
        // _objectPooler.SpawnFromPool(horseBreed, new Vector3(0, Random.Range(250, 1500)));
        StartCoroutine(RemoveAfterSeconds(lagSeconds, horseBreed));
    }
    
    //https://forum.unity.com/threads/hide-object-after-time.291287/
    IEnumerator RemoveAfterSeconds(float seconds, string horseBreed)
    {
        yield return new WaitForSeconds(seconds);
        _objectPooler.SpawnFromPool(horseBreed, new Vector3(0, Random.Range(250, 1500)));
    }

    public static void DestroyObject(string fingerPointerLabel)
    {
        var fingerPointer = GameObject.Find(fingerPointerLabel);
        if (fingerPointer != null)
        {
            Destroy(fingerPointer);
        }
    }
}