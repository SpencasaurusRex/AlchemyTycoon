using System.Collections.Generic;
using UnityEngine;

public class TagComponent : MonoBehaviour 
{
    // Configuration variables
    public List<Tag> StartingTags;

    // Runtime variables
    HashSet<Tag> Tags = new HashSet<Tag>();

    void Start()
    {
        foreach (var tag in StartingTags)
        {
            AddTag(tag);
        }
    }

    public void AddTag(Tag tag)
    {
        Tags.Add(tag);
    }

    public void RemoveTag(Tag tag)
    {
        Tags.Remove(tag);
    }

    public bool HasTag(Tag tag) => Tags.Contains(tag);

    public bool HasAny(List<Tag> tags)
    {
        foreach (var tag in tags)
        {
            if (HasTag(tag)) return true;
        }
        return false;
    }
}

public enum Tag 
{
    Ingredient,
    Tool,
}