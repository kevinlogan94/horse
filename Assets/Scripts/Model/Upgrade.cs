using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public string Name;
    public string Description;
    public int LevelRequirement;
    public int Cost;
    public int DynamicCost;
    public int Level;
    public int HighestLevel;
    public int UpgradeBy;
    public Helper HelperToUpgrade;
    public Sprite Artwork;
}
