using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryTelling", menuName = "Narrative/StoryTelling/Dialog")]
public class StoryTelling : ScriptableObject
{
    public string title;
    public string description;
    public bool status = true;


}
