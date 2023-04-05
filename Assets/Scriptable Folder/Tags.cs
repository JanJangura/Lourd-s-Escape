using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tag", menuName = "Tags/New Tag")]
public class Tags : ScriptableObject
{
    public string Name => name;
}
