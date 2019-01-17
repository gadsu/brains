using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDiction : MonoBehaviour
{
    protected Dictionary<string, string> _dict;
    KeyNotFoundException _key = new KeyNotFoundException ("<color=yellow>Key: ");

    private void Awake()
    {
        _dict = new Dictionary<string, string>();
    }

    public void AddToDictionary(string name, string behavior)
    {
        try
        {
            if (!_dict.ContainsKey(name))
            {
                _dict.Add(name, behavior);
            }
            else throw _key;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message + name + ", " + behavior + "</color>");
        }
    }
}
