using UnityEngine;

[CreateAssetMenu(fileName = "New Chapter", menuName = "Chapter")]
public class Chapter : ScriptableObject
{
    public string Name;
    public int Number;
    public bool SceneViewed;
    public string[] Quotes;
}
