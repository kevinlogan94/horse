using UnityEngine;

[CreateAssetMenu(fileName = "New Helper", menuName = "Helper")]
public class Helper : ScriptableObject
{
    public string Name;
    public string Description;
    public int LevelRequirement;
    public int Cost;
    public int DynamicCost;
    public int Increment;
    public int DynamicIncrement;
    public int AmountOwned;
    public string HorseBreed;
    public Sprite Artwork;
}