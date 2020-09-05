using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Horse Object", menuName = "Horse Object")]
public class HorseObject : ScriptableObject
{
    public string Name;
    public string Description;
    public GameObject PreFab;
}
