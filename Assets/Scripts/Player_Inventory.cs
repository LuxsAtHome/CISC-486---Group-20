using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private HashSet<string> keys = new HashSet<string>();
    public void AddKey(string id) => keys.Add(id);
    public bool HasKey(string id) => keys.Contains(id);
    public bool UseKey(string id)
    {
        if (!keys.Contains(id)) return false;
        keys.Remove(id);  // remove if you want one-time use
        return true;
    }
}
