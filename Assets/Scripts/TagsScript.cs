using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsScript : MonoBehaviour
{
    [SerializeField] private List<Tags> tags;

    public List<Tags> All => tags; 

    public bool HasTag(Tags t)
    {
        return tags.Contains(t);
    }

    public bool HasTag(string tagName)
    {
        return tags.Exists(t => t.Name.Equals(tagName, System.StringComparison.InvariantCultureIgnoreCase));
    }
}
