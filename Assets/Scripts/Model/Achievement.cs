﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string Name;
    public string Title;
    public string RewardDescription;
    public Sprite Artwork;
}