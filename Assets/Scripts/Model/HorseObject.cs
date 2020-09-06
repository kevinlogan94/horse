using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Horse Object", menuName = "Horse Object")]
public class HorseObject : ScriptableObject
{
    public string Name;
    public string Description;
    public int HorseAnimationInt;
}
