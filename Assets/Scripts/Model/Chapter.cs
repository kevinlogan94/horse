using UnityEngine;

[CreateAssetMenu(fileName = "New Chapter", menuName = "Chapter")]
public class Chapter : ScriptableObject
{
    public string Name;
    public int Number;
    public int LevelRequirement;
    public bool SceneViewed;
    public string[] Quotes;
}
