using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDiction : MonoBehaviour
{
    protected Dictionary<string, string> mdict;
    readonly KeyNotFoundException _key = new KeyNotFoundException ("<color=yellow>Key: ");

    private void Awake()
    {
        mdict = new Dictionary<string, string>();
    }

    public void AddToDictionary(string name, string behavior)
    {
        try
        {
            if (!mdict.ContainsKey(name))
            {
                mdict.Add(name, behavior);
            }
            else throw _key;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k.Message + name + ", " + behavior + "</color>");
        }
    }
}
